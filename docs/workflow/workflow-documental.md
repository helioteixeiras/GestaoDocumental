# Workflow documental

## Objetivo

O workflow gere o ciclo de vida de um documento: tramitação entre direções, aprovação, rejeição e registo histórico. A implementação actual usa constantes em código e persiste eventos em `TramitacaoDocumento` e `DocumentoHistorico`.

---

## Estados documentais

Constantes em `DocumentoWorkflowConstants`:

| Estado | Constante | Uso |
|--------|-----------|-----|
| Pendente | `EstadoPendente` | Estado inicial esperado (criado automaticamente se não existir na BD) |
| Em Tramitacao | `EstadoEmTramitacao` | Após encaminhamento |
| Aprovado | `EstadoAprovado` | Após aprovação |
| Rejeitado | `EstadoRejeitado` | Após rejeição |

**Importante:** se o estado não existir na tabela `EstadoDocumento`, o método `EnsureEstadoDocumentoAsync` **cria-o automaticamente** durante a operação de workflow.

**Limitação:** documentos de seed de teste podem ter estado `"Estado Teste Auth"`, que **não** entra nos contadores do dashboard (Pendente, Em Tramitacao, etc.) até serem tramitados para estados reconhecidos.

---

## Operações de workflow

### Endpoints (TramitacaoDocumentoController)

| Operação | Método | URL | Policy |
|----------|--------|-----|--------|
| Aprovar | POST | `/api/TramitacaoDocumento/{documentoId}/aprovar` | `PodeAprovarDocumentos` |
| Rejeitar | POST | `/api/TramitacaoDocumento/{documentoId}/rejeitar` | `PodeAprovarDocumentos` |
| Encaminhar | POST | `/api/TramitacaoDocumento/{documentoId}/encaminhar` | `PodeGerirDocumentos` |

Corpo típico (aprovar/rejeitar):

```json
{
  "observacao": "Texto opcional, máx. 500 caracteres"
}
```

Corpo de encaminhamento:

```json
{
  "direcaoDestinoId": 1,
  "colaboradorDestinoId": 2,
  "observacao": "Opcional"
}
```

### Consultar timeline

```
GET /api/Documento/{id}/workflow
```

Policy: `PodeConsultarDocumentos`

Devolve resumo, listas separadas de histórico e tramitações, e **timeline** unificada ordenada por data.

---

## Regras implementadas

### Aprovação

- Não permitida se documento **já está Aprovado**
- Não permitida se documento **já está Aprovado** (operação bloqueada)
- Se documento está **Rejeitado**, exige **encaminhamento prévio** antes de nova aprovação
- Cria registo em `TramitacaoDocumento` (estado `"Aprovado"`)
- Actualiza `EstadoDocumento` do documento para `"Aprovado"`
- Regista histórico com ação `"Aprovacao"`

### Rejeição

- Não permitida se documento **já está Rejeitado**
- Não permitida se documento **já está Aprovado**
- Actualiza estado para `"Rejeitado"`
- Regista tramitação e histórico com ação `"Rejeicao"`

### Encaminhamento

- **Não permitido** se documento já está **Aprovado**
- `DirecaoDestinoId` obrigatório (> 0)
- Actualiza estado para `"Em Tramitacao"`
- Cria tramitação com estado `"Encaminhado"`, origem/destino e colaboradores
- Regista histórico com ação `"Encaminhamento"`

### Utilizador no histórico

`DocumentoHistorico.UtilizadorId` recebe:

- `UsuarioSistema.ColaboradorId`, ou
- `Documento.ColaboradorCriadorId` se o utilizador não tiver colaborador (caso típico: `admin`)

---

## Histórico (`DocumentoHistorico`)

Além do workflow, o histórico regista acções de ficheiros:

| Acção | Origem |
|-------|--------|
| `Aprovacao`, `Rejeicao`, `Encaminhamento` | Workflow |
| `UploadArquivo` | Upload |
| `DownloadArquivo` | Download da última versão |
| `DownloadArquivoVersao` | Download de anexo específico |
| `RemocaoAnexo` | Soft delete de anexo |

---

## Tramitação (`TramitacaoDocumento`)

Cada operação de workflow cria um registo com:

- `DocumentoId`, direcções origem/destino
- Colaborador origem (do utilizador autenticado) e destino (no encaminhamento)
- `Estado` da tramitação: `Aprovado`, `Rejeitado` ou `Encaminhado`
- `DataEnvio`, `DataRececao`, `Observacao`

---

## Timeline

O endpoint `GET /api/Documento/{id}/workflow` constrói a timeline unindo:

1. **Eventos de histórico** (`TipoEvento = "Historico"`) — inclui uploads, downloads, workflow
2. **Eventos de tramitação** (`TipoEvento = "Tramitacao"`) — usa `DataEnvio`

Ordenação: data ascendente, depois tipo.

O DTO inclui:

- **Resumo:** id, número, título, estado actual, totais, última acção
- **ListaHistorico:** todos os registos de `DocumentoHistorico`
- **ListaTramitacoes:** registos de `TramitacaoDocumento`
- **Timeline:** visão cronológica combinada

---

## Resposta de operações de workflow

`DocumentoWorkflowResultDto` devolve:

- `DocumentoId`
- `EstadoDocumento` (nome actualizado)
- `TramitacaoId`
- `Acao`
- `DataAcao`
- `Observacao`

---

## Validações

FluentValidation nos DTOs de aprovar/rejeitar/encaminhar (ex.: observação máx. 500 caracteres).

Erros de regra de negócio → `InvalidOperationException` → **HTTP 400**

Documento inexistente → `KeyNotFoundException` → **HTTP 404**

---

## Limitações actuais

- Workflow **não configurável** por tabela — estados e transições estão em código
- CRUD genérico de `TramitacaoDocumento` e `DocumentoHistorico` existe com `[Authorize]` apenas (sem policy por role) — uso administrativo directo, não faz parte do fluxo principal
- Não há notificações, tarefas pendentes nem fila de aprovação por utilizador
