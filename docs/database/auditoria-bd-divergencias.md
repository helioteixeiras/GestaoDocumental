# Auditoria BD — Relatório de divergências (pré-correção)

**Data:** 2026-05-25  
**Base de dados:** `GestaoDocumental` em `DESKTOP-BIGPTQI\SQLEXPRESS`  
**Fonte da verdade:** estrutura real SQL Server (INFORMATION_SCHEMA + sys.*)

---

## Tabelas analisadas (24 + histórico EF)

| # | Tabela na BD | DbSet / Entidade no código |
|---|--------------|----------------------------|
| 1 | CategoriaDocumento | ❌ Ausente |
| 2 | ClassificacaoDocumento | ✅ |
| 3 | Colaborador | ✅ (desatualizada) |
| 4 | Departamento | ✅ |
| 5 | Direcao | ✅ |
| 6 | Documento | ✅ (desatualizada — **crítico**) |
| 7 | DocumentoAnexo | ✅ (max lengths) |
| 8 | DocumentoComentario | ❌ Ausente |
| 9 | DocumentoHistorico | ✅ |
| 10 | EstadoColaborador | ✅ |
| 11 | EstadoDocumento | ✅ |
| 12 | EstadoLogin | ✅ |
| 13 | Fornecedor | ✅ (max lengths) |
| 14 | Genero | ✅ |
| 15 | Municipio | ✅ (desatualizada) |
| 16 | Pais | ✅ (desatualizada) |
| 17 | Perfil | ✅ |
| 18 | PostoTrabalho | ✅ |
| 19 | Provincia | ✅ (desatualizada) |
| 20 | TipoDocumento | ✅ (desatualizada — **crítico**) |
| 21 | TipoDocumentoColaborador | ✅ |
| 22 | TramitacaoDocumento | ✅ |
| 23 | UsuarioSistema | ✅ (desatualizada) |
| 24 | __EFMigrationsHistory | N/A |

---

## Divergências críticas

### 1. Documento — coluna `Titulo` no código vs `Assunto` na BD

| Aspecto | BD (real) | Código (EF/entidade) |
|---------|-----------|----------------------|
| Assunto/título | `Assunto` nvarchar(500) NOT NULL | Propriedade `Titulo` mapeada para coluna inexistente |
| ReferenciaInterna | nvarchar(200) NULL | ❌ Ausente |
| Etiqueta | nvarchar(200) NULL | ❌ Ausente |
| DataCriacao | `datetime` | `BaseEntity.DataCriacao` datetime2 |
| Max lengths | Maiores (ex. NumeroDocumento 200) | Fluent API menores (100) |

**Risco:** INSERT/SELECT em `Documento` falham ou ignoram campos quando a tabela tiver dados. Tabela actualmente vazia (0 linhas).

### 2. TipoDocumento — modelo incompleto

| Coluna BD | Código |
|-----------|--------|
| `Codigo` nvarchar(60) NOT NULL, UQ | ❌ Ausente |
| `CategoriaDocumentoId` int NOT NULL, FK | ❌ Ausente |
| `Nome` nvarchar(200) | ✅ |
| UQ `(Nome, CategoriaDocumentoId)` | ❌ Não reflectido |

**Risco:** Criação de tipos de documento via API/seed viola NOT NULL/FK na BD.

### 3. Tabelas na BD sem entidade/DbSet

| Tabela | Colunas principais | FKs |
|--------|-------------------|-----|
| **CategoriaDocumento** | Id, Nome, Codigo, Ativo, DataCriacao, DataAtualizacao | — (UQ Codigo) |
| **DocumentoComentario** | Id, DocumentoId, UsuarioSistemaId, Comentario, Ativo, datas | FK Documento (sem FK UsuarioSistema na BD) |

**Dados:** 20 categorias; 0 comentários.

---

## Divergências por tabela (prioritárias)

### Pais

| Coluna BD | Código |
|-----------|--------|
| SiglaISO2, CodigoIso3, Capital, IndicativoTelefonico | ❌ Ausentes |
| Nome max 100 | Fluent 50 |

### Provincia

| Coluna BD | Código |
|-----------|--------|
| Sigla, CodigoINE | ❌ Ausentes |
| UQ Sigla, UQ CodigoINE | ❌ Não reflectidos |
| Nome max 120 | Fluent 60 |

### Municipio

| Coluna BD | Código |
|-----------|--------|
| Sigla, CodigoINE | ❌ Ausentes |
| UX (ProvinciaId, Nome) | ❌ Não reflectido |

### Colaborador

| Coluna BD | Código |
|-----------|--------|
| DepartamentoId NULL, FK Colaborador_Departamento | ❌ Ausente |
| Max lengths maiores | Fluent menores |

### UsuarioSistema

| Coluna BD | Código |
|-----------|--------|
| PasswordSalt nvarchar(1000) NULL | ❌ Ausente |
| UQ ColaboradorId **sem** filter | Código: filter `IS NOT NULL` (diverge) |
| Username max 200 | Fluent 100 |
| Email max 300 | Fluent 150 |

### DocumentoAnexo / DocumentoHistorico / TramitacaoDocumento

- Colunas alinhadas funcionalmente; **max lengths** na Fluent API inferiores aos da BD (500→1000 em anexos, etc.).

### ClassificacaoDocumento, Direcao, Departamento, Perfil, etc.

- Estrutura base OK; vários **max lengths** e defaults diferem (impacto baixo se não truncar).

---

## FKs na BD não reflectidas no DbContext

| FK | Tabela |
|----|--------|
| FK_TipoDocumento_CategoriaDocumento | TipoDocumento → CategoriaDocumento |
| FK_Colaborador_Departamento | Colaborador → Departamento |
| FK_DocumentoComentario_Documento | DocumentoComentario → Documento |

FK ausente na BD (mas coluna existe): `DocumentoComentario.UsuarioSistemaId` sem constraint.

---

## Índices únicos na BD não reflectidos

| Tabela | Índice |
|--------|--------|
| CategoriaDocumento | UQ_CategoriaDocumento_Codigo |
| TipoDocumento | UQ_TipoDocumento_Codigo, UQ_TipoDocumento_Nome_Categoria |
| Municipio | UX_Municipio_Provincia_Nome |
| Provincia | UQ_Provincia_CodigoINE, UQ_Provincia_Sigla |

---

## Snapshot EF / Migrations

O `GestaoDocumentalDbContextModelSnapshot` reflecte schema **antigo** (ex.: `Documento.Titulo`, `TipoDocumento` só com Nome). **Não corresponde** à BD live após alterações manuais.

**Alteração não aplicada nesta fase:** nova migration EF (regra do projecto: não criar migration sem aprovação). Sincronização via entidades + Fluent API apenas.

---

## Plano de correção (código)

1. Criar entidades `CategoriaDocumento`, `DocumentoComentario`
2. Actualizar entidades desatualizadas (Documento, TipoDocumento, Pais, Provincia, Municipio, Colaborador, UsuarioSistema)
3. Actualizar `GestaoDocumentalDbContext` (DbSets, Fluent API, FKs, índices, max lengths)
4. Actualizar DTOs, AutoMapper, services, controllers, seed
5. `dotnet build` para validar

---

## Alterações aplicadas no código (pós-auditoria)

- Entidades novas: `CategoriaDocumento`, `DocumentoComentario`
- `Documento`: `Titulo` → `Assunto`, +`ReferenciaInterna`, +`Etiqueta`
- `TipoDocumento`: +`Codigo`, +`CategoriaDocumentoId`, navegação
- `Pais`, `Provincia`, `Municipio`, `Colaborador`, `UsuarioSistema`: colunas e FKs alinhadas
- `GestaoDocumentalDbContext`: DbSets, Fluent API, índices e max lengths
- DTOs, AutoMapper, seed, `CategoriaDocumentoController` + service
- `dotnet build` — **sucesso (0 erros)**

## Alterações não aplicadas (justificação)

| Item | Motivo |
|------|--------|
| Nova migration EF | Regra do projecto — snapshot legado não alterado |
| `DocumentoComentarioController` | Tabela vazia; entidade/DbSet prontos para fase seguinte |
| FK `UsuarioSistema` em `DocumentoComentario` na BD | Não existe na BD — relação mapeada só no EF |
| Renomear propriedade API `Titulo` mantida | Substituída por `Assunto` (alinhamento BD) — breaking change documentado |

*Relatório completo entregue na conversa.*
