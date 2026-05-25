# Migrations e base legada

## Contexto

A base de dados **já existia** antes da adopção formal do EF Core Migrations. O projecto adoptou uma estratégia de **baseline**: reconhecer o schema existente sem o recriar, e aplicar apenas alterações incrementais necessárias.

**Regra de ouro:** não apagar nem recriar a base de dados legada.

---

## Estratégia de baseline

A migration **`InitialBaseline`** tem métodos `Up` e `Down` **vazios**. Serve para:

1. Marcar no histórico EF que o schema actual já estava aplicado
2. Permitir migrations futuras sem tentar recriar todas as tabelas legadas

Fluxo típico usado na estabilização:

1. BD legada já populada e funcional
2. Gerar snapshot EF alinhado ao schema existente
3. Criar `InitialBaseline` vazia como ponto de partida
4. Migrations seguintes apenas com **delta** real

---

## Migrations actuais

Localização: `GestaoDocumental.Infrastructure/Data/Migrations/`

| Migration | Data (nome) | O que faz |
|-----------|-------------|-----------|
| **InitialBaseline** | `20260525135510` | Baseline vazia — schema legado já existente |
| **AddBaseEntityAuditFields** | `20260525142017` | Adiciona `Ativo`, `DataCriacao`, `DataAtualizacao` em várias tabelas; campos em `UsuarioSistema` (`Email`, `PerfilId`, FK); índice único de email |
| **FixUsuarioSistemaColaboradorNullable** | `20260525142747` | Torna `UsuarioSistema.ColaboradorId` **nullable** (admin sem colaborador) |

Snapshot: `GestaoDocumentalDbContextModelSnapshot.cs`

---

## Por que não apagar a BD

| Risco | Consequência |
|-------|--------------|
| Perda de dados legados | Informação histórica irreversível |
| Quebra de referências | Documentos, colaboradores, anexos existentes |
| Re-seed incompleto | `IdentityDataSeeder` não repõe todo o legado |
| Ambiente partilhado | Outros developers perdem estado local |

Alterações devem ser **incrementais** via novas migrations ou scripts aprovados.

---

## BaseEntity e auditoria

`BaseEntity` no Domain define:

```csharp
Id, DataCriacao, DataAtualizacao, Ativo
```

A migration `AddBaseEntityAuditFields` alinhou tabelas legadas a este padrão. Entidades como `DocumentoAnexo` usam `Ativo` para soft delete.

---

## Como criar migration futura com segurança

### Antes de começar

1. Confirmar que a BD local reflecte migrations já aplicadas
2. Fazer **backup** da BD (produção ou ambiente partilhado)
3. Obter aprovação se a alteração afectar tabelas críticas
4. **Nunca editar** migrations já commitadas/aplicadas noutros ambientes

### Passos recomendados

Na pasta do projecto Infrastructure (ou via solution):

```powershell
cd backend\GestaoDocumental.Infrastructure

dotnet ef migrations add NomeDescritivoDaAlteracao `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj `
  --context GestaoDocumentalDbContext
```

Rever o ficheiro gerado:

- Confirmar que só contém alterações pretendidas
- Evitar `DropTable` acidental
- Verificar defaults em colunas NOT NULL em tabelas com dados

Aplicar localmente:

```powershell
dotnet ef database update `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj `
  --context GestaoDocumentalDbContext
```

### Boas práticas

- Preferir colunas nullable ou defaults seguros em tabelas populadas
- Migrations pequenas e focadas (uma preocupação por migration)
- Testar `dotnet ef database update` numa cópia da BD antes de produção
- Documentar breaking changes

---

## Comandos EF Core úteis

### Listar migrations

```powershell
dotnet ef migrations list `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj
```

### Ver SQL que seria executado

```powershell
dotnet ef migrations script `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj
```

### Reverter para migration anterior (cuidado)

```powershell
dotnet ef database update NomeMigrationAnterior `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj
```

### Gerar script entre duas migrations

```powershell
dotnet ef migrations script InitialBaseline FixUsuarioSistemaColaboradorNullable `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj
```

---

## DbContext

- Classe: `GestaoDocumentalDbContext`
- Local: `Infrastructure/Data/Context/`
- Entidades mapeadas em `Entities/Legacy` e configurações EF

Connection string: secção `ConnectionStrings:DefaultConnection` (configurada no projecto Api — **não alterar appsettings sem necessidade**).

---

## Limitações e pendências

- Colunas futuras sugeridas (não implementadas): `Versao` dedicada em anexo, `TipoMime`, `RemovidoEm`
- Histórico de downloads usa parsing de `Observacao` — migration futura poderia normalizar metadados
- Não há pipeline automatizado de migrations em CI/CD (ver roadmap)
