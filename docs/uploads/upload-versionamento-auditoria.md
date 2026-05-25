# Upload, versionamento e auditoria

## Visão geral

Os ficheiros físicos são guardados em disco local; os metadados ficam na entidade **`DocumentoAnexo`**. Cada upload cria um novo anexo (nova versão). Downloads são protegidos por JWT e auditados em `DocumentoHistorico`.

---

## Upload físico

### Endpoint

```
POST /api/Documento/{id}/upload
Content-Type: multipart/form-data
Campo: file
```

Policy: **`PodeGerirDocumentos`** (Administrador, Operador)

Limite no controller: **10 MB** (`RequestSizeLimit` / `RequestFormLimits`).

### Fluxo

1. Valida ficheiro (`DocumentoFileValidator`)
2. Confirma documento e utilizador activo
3. Grava ficheiro via `LocalFileStorageService`
4. Cria `DocumentoAnexo` na BD
5. Regista histórico `UploadArquivo`
6. Devolve `DocumentoUploadResultDto` com `AnexoId`, hash, versão, etc.

### Storage local

Configuração: secção **`Storage`** (`StorageSettings`)

| Setting | Default | Descrição |
|---------|---------|-----------|
| `BasePath` | `storage/documentos` | Pasta relativa à raiz da API |
| `MaxFileSizeMb` | `10` | Tamanho máximo |
| `AllowedExtensions` | `.pdf`, `.doc`, `.docx`, `.xls`, `.xlsx`, `.png`, `.jpg`, `.jpeg` | Extensões permitidas |

Estrutura no disco:

```
storage/documentos/
  └── {documentoId}/
        └── {guid}.{ext}
```

Cada ficheiro guarda também **hash SHA-256** calculado durante a gravação.

---

## Entidade DocumentoAnexo

Campos relevantes (herda `BaseEntity`):

| Campo | Descrição |
|-------|-----------|
| `DocumentoId` | Documento pai |
| `GuidFicheiro` | Identificador único do ficheiro |
| `NomeOriginal` | Nome enviado pelo utilizador |
| `NomeFisico` | Nome no disco (`{guid}.ext`) |
| `Extensao` | Extensão normalizada |
| `Caminho` | Caminho relativo (`{documentoId}/{guid}.ext`) |
| `HashSha256` | Integridade |
| `Tamanho` | Bytes |
| `DataUpload` | Data do upload |
| `Ativo` | `false` após soft delete |

**Limitação:** não existe coluna `Versao` na BD — a versão é **calculada em runtime** pela ordem de `DataUpload` + `Id` entre anexos activos.

---

## Validações de ficheiro

`DocumentoFileValidator` verifica:

- Nome não vazio
- Tamanho > 0
- Tamanho ≤ `MaxFileSizeMb`
- Extensão na lista permitida

Falhas → `ValidationException` → **HTTP 400**

---

## Versionamento v1

### Listar anexos

```
GET /api/Documento/{id}/anexos
```

Policy: `PodeConsultarDocumentos`

Resposta `DocumentoAnexoListDto`:

- `TotalAnexos`, `UltimaVersao`
- Lista com `Versao`, `EhUltimaVersao`, `HashSha256`, `DownloadUrl` relativa

Versão = posição cronológica entre anexos **activos** (1 = mais antigo).

### Download da última versão

```
GET /api/Documento/{id}/download
```

Policy: `PodeConsultarDocumentos`

- Selecciona o anexo activo mais recente (`DataUpload` desc)
- Regista histórico `DownloadArquivo`
- Devolve ficheiro ou **404** se não existir anexo/ficheiro

### Download por anexo (versão específica)

```
GET /api/Documento/{id}/anexos/{anexoId}/download
```

Policy: `PodeConsultarDocumentos`

- Regista histórico `DownloadArquivoVersao`
- Observação inclui metadados parseáveis (ver auditoria)

---

## Soft delete de anexos

```
DELETE /api/Documento/{id}/anexos/{anexoId}
```

Policy: **`PodeGerirDocumentos`**

Comportamento:

- Marca `Ativo = false` e `DataAtualizacao`
- Regista histórico `RemocaoAnexo`
- Anexos inactivos **não aparecem** em listagens nem downloads
- Segunda remoção do mesmo anexo → **404**
- **O ficheiro físico permanece no disco** (limitação v1)

---

## Auditoria de downloads

Cada download regista entrada em `DocumentoHistorico`:

| Acção | Quando |
|-------|--------|
| `DownloadArquivo` | Download da última versão |
| `DownloadArquivoVersao` | Download de anexo específico |

Formato da observação (parseável):

```
{nomeOriginal} | AnexoId={id} | Versao={n} | HashSha256={hash}
```

`DownloadHistoricoObservacaoParser` extrai estes campos para relatórios.

`UtilizadorId` no histórico = colaborador do utilizador autenticado (ou fallback do criador do documento).

---

## Relatório de downloads

### Listagem paginada

```
GET /api/Documento/{id}/downloads
```

Policy: `PodeConsultarDocumentos`

Query params:

| Param | Descrição |
|-------|-----------|
| `dataInicio`, `dataFim` | Filtro por intervalo |
| `usuarioId` | Filtro por colaborador (`UtilizadorId`) |
| `acao` | `DownloadArquivo` ou `DownloadArquivoVersao` (vazio = ambas) |
| `page` | Default 1 |
| `pageSize` | Default 20, máx. 100 |

Resposta inclui paginação (`TotalPages`, `HasNextPage`, etc.).

### Resumo agregado

```
GET /api/Documento/{id}/downloads/resumo
```

Policy: `PodeConsultarDocumentos`

Agregações:

- Total e intervalo (primeiro/último download)
- Por acção, por utilizador, por dia
- Ficheiros mais baixados (nome, anexo, versão)

---

## Exportação CSV

```
GET /api/Documento/{id}/downloads/export/csv
```

Policy: `PodeConsultarDocumentos`

Filtros: `dataInicio`, `dataFim`, `usuarioId`, `acao`

Formato:

- UTF-8 com BOM
- Separador **`;`**
- Colunas: HistoricoId, DataAcao, Acao, UsuarioId, UsuarioNome, AnexoId, Versao, HashSha256, Observacao
- Nome do ficheiro: `downloads-documento-{id}-{timestamp}.csv`

Implementação: `CsvExportService` em Infrastructure.

---

## Controller genérico DocumentoAnexo

Existe `DocumentoAnexoController` com CRUD REST genérico (`[Authorize]` apenas). O fluxo recomendado para anexos é via **`DocumentoController`** (upload, listagem, download, remoção).

---

## Limitações actuais

- Storage apenas **local** (sem S3/Azure Blob)
- Sem restore de anexos removidos
- Soft delete não apaga ficheiro físico
- Versão não persistida na BD
- Sem validação de tipo MIME além da extensão
- `DocumentoAnexoController` CRUD directo sem policies granulares
