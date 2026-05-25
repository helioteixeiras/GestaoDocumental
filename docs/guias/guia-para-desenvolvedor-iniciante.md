# Guia para desenvolvedor iniciante

Bem-vindo ao **GestãoDocumental**. Este guia ajuda quem está a começar no projecto — ou quem vem de outras stacks — a orientar-se sem partir nada importante.

---

## O que precisa saber primeiro

1. **É uma API .NET 9** — não há frontend ainda; testa-se via Swagger ou ferramentas como Postman.
2. **Há uma base de dados legada** — não apague nem recrie a BD para “facilitar”.
3. **A arquitetura tem camadas** — Controller → Service → Repository → SQL Server.
4. **Quase tudo exige login JWT** — excepto `POST /api/Auth/login`.
5. **Existem 3 roles** — Administrador, Operador, Consulta — com permissões diferentes.

Leia primeiro:

- [README](../README.md) — mapa geral
- [Camadas em termos simples](../arquitetura/02-camadas-em-termos-simples.md) — onde mexer
- [Como executar localmente](../deploy/como-executar-localmente.md) — pôr a API a correr

---

## Conceitos mínimos

| Termo | Significado rápido |
|-------|-------------------|
| **Controller** | Ficheiro que responde a URLs HTTP |
| **Service** | Onde vive a lógica de negócio |
| **Repository** | Onde se lê/grava na base de dados |
| **DTO** | Objecto simples para pedidos/respostas API |
| **Entity** | Representação de uma tabela |
| **Migration** | Alteração versionada ao schema SQL |
| **Policy** | Regra “quem pode aceder” além do login |
| **JWT** | Token de sessão após login |

---

## Como encontrar onde mexer

### Pergunta: “Onde está o endpoint X?”

1. Abra `backend/GestaoDocumental.Api/Controllers/`
2. Procure pelo nome (ex.: `DocumentoController.cs`)
3. O método com `[HttpGet]`, `[HttpPost]`, etc. é o endpoint

### Pergunta: “Onde está a regra de negócio?”

1. Veja qual service o controller injecta (ex.: `IDocumentoService`)
2. Abra `backend/GestaoDocumental.Application/Services/`
3. A implementação está no ficheiro homónimo (ex.: `DocumentoService.cs`)

### Pergunta: “Onde está a query SQL/EF?”

1. Identifique o repositório usado pelo service
2. Abra `backend/GestaoDocumental.Infrastructure/Data/Repositories/`

### Pergunta: “Onde altero permissões?”

1. Roles: `GestaoDocumental.Shared/Security/AppRoles.cs`
2. Policies: `AppPolicies.cs` + registo em `Program.cs`
3. Por endpoint: atributos `[Authorize(Policy = ...)]` nos controllers

---

## Ciclo correcto para alterar algo

### Alteração pequena (ex.: nova validação)

1. **Branch Git** (se usar equipa): `git checkout -b fix/validacao-upload`
2. Alterar ficheiro na camada correcta (validação → `Application/Validators/`)
3. Compilar: `dotnet build GestaoDocumental.sln`
4. Executar API e testar no Swagger
5. Commit com mensagem clara (quando pedido)

### Alteração média (ex.: novo endpoint)

1. DTO em `Application/DTOs/`
2. Método no service (+ interface)
3. Implementação no repository se precisar de query nova
4. Action no controller com policy correcta
5. Registar DI se criou interfaces novas
6. Testar com **admin**, **operador** e **consulta**

### Alteração de base de dados

1. **Parar** — ler [Migrations e base legada](../database/migrations-e-base-legada.md)
2. Alterar entidade no Domain
3. `dotnet ef migrations add ...` (nunca editar migrations antigas)
4. Rever SQL gerado
5. Backup + `dotnet ef database update` em ambiente de teste

---

## Como usar Git antes/depois de alterações

### Antes de começar

```powershell
git status
git pull
```

Veja se alguém alterou migrations ou `Program.cs`.

### Durante o trabalho

- Commits pequenos e focados
- Não commitar secrets (connection strings de produção, chaves JWT reais)
- Não commitar pasta `storage/documentos` com ficheiros de teste (se estiver no `.gitignore`, respeitar)

### Antes de push/PR

```powershell
dotnet build GestaoDocumental.sln
git status
git diff
```

Descreva **o que** mudou e **porquê**.

---

## Como pedir ajuda à IA / Cursor sem destruir o projecto

### Faça pedidos específicos

**Bom:**

> “Adicionar validação de tamanho máximo no DTO de encaminhamento, sem alterar migrations.”

**Mau:**

> “Refaz a base de dados e simplifica tudo.”

### Declare restrições explícitas

Inclua sempre que aplicável:

- Não apagar/recriar BD
- Não alterar migrations existentes
- Não mexer em appsettings
- Manter Clean Architecture
- Apenas documentação / apenas backend / apenas ficheiro X

### Peça diff pequeno

- Uma funcionalidade de cada vez
- Revise o diff antes de aceitar
- Se a IA sugerir apagar ficheiros de migration ou `Program.cs` inteiro, **pare e questione**

### Verifique camada

Se a IA colocar EF Core num Service da Application, **corrija** — belongs in Infrastructure.

### Teste depois da IA

1. `dotnet build`
2. Swagger + login admin
3. Teste o endpoint afectado
4. Se envolver roles, teste operador e consulta

### Sinais de alerta (rejeitar ou pedir correcção)

- `DropDatabase`, `EnsureDeleted`, recriar migrations do zero
- Controllers com lógica SQL directa
- Endpoints sem `[Authorize]` em operações sensíveis
- Funcionalidades inventadas que não pediu
- Alteração massiva de nomes sem contexto

---

## Utilizadores para testar permissões

| User | Password | O que deve conseguir |
|------|----------|----------------------|
| admin | Admin123* | Tudo, incluindo utilizadores |
| operador | Operador123* | Documentos, upload, aprovar, encaminhar |
| consulta | Consulta123* | Ver, download, dashboard — **não** upload nem aprovar |

Se consulta receber **403** num POST de upload, está correcto.

---

## Recursos dentro do repositório

| Tema | Documento |
|------|-----------|
| Endpoints | [endpoints-principais.md](../api/endpoints-principais.md) |
| Segurança | [jwt-roles-policies.md](../seguranca/jwt-roles-policies.md) |
| Workflow | [workflow-documental.md](../workflow/workflow-documental.md) |
| Ficheiros | [upload-versionamento-auditoria.md](../uploads/upload-versionamento-auditoria.md) |
| BD | [migrations-e-base-legada.md](../database/migrations-e-base-legada.md) |
| Futuro | [proximos-passos.md](../roadmap/proximos-passos.md) |

---

## Mensagem final

Errar faz parte — **desde que compile, teste e não apague a BD legada**. Em dúvida, pergunte à equipa ou à IA com restrições claras e revise sempre o diff antes de merge.
