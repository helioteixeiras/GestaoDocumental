# Camadas em termos simples

Este documento explica cada parte do backend com linguagem acessível, para quem está a entrar no projeto ou precisa de saber **onde mexer**.

## Analogia geral: restaurante

Imagine o backend como um restaurante:

| Camada | Analogia | Função |
|--------|----------|--------|
| **Api** | Sala e empregado de mesa | Recebe o pedido do cliente e entrega a resposta |
| **Application** | Cozinha | Prepara o prato seguindo receitas (regras) |
| **Domain** | Cardápio e ingredientes base | Define o que existe (documento, anexo, utilizador) |
| **Infrastructure** | Fornecedores e armazém | Busca dados na BD, grava ficheiros, gera tokens |
| **Shared** | Manual partilhado | Nomes de roles, policies, configurações comuns |

O cliente (Swagger ou futuro Blazor) **nunca fala diretamente** com a base de dados.

---

## Api — a porta de entrada

**O que é:** projeto ASP.NET que expõe URLs como `/api/Documento`, `/api/Auth/login`.

**O que faz:**

- Recebe HTTP (GET, POST, PUT, DELETE)
- Verifica JWT e policies (`[Authorize]`)
- Chama services e devolve JSON ou ficheiros

**Onde mexer quando quiser:**

| Objetivo | Onde |
|----------|------|
| Novo endpoint HTTP | Novo método no controller ou novo controller em `Controllers/` |
| Exigir role/policy | Atributo `[Authorize(Policy = ...)]` no controller ou action |
| Configurar Swagger/JWT no arranque | `Program.cs` |
| Tratamento global de erros | `Middlewares/GlobalExceptionMiddleware.cs` |

**Evite:** colocar SQL ou regras de negócio longas nos controllers.

---

## Application — a lógica de negócio

**O que é:** o “cérebro” do sistema. Contém services, DTOs e validações.

**O que faz:**

- Implementa casos de uso: aprovar documento, fazer upload, gerar relatório
- Valida dados de entrada (FluentValidation)
- Orquestra repositórios e storage

**Onde mexer quando quiser:**

| Objetivo | Onde |
|----------|------|
| Alterar regra de negócio | `Services/` (ex.: `DocumentoService.cs`, `TramitacaoDocumentoService.cs`) |
| Novo DTO de entrada/saída | `DTOs/` |
| Validar campos de um pedido | `Validators/` |
| Constantes de workflow | `Common/DocumentoWorkflowConstants.cs` |
| Registar novo service | `DependencyInjection/ApplicationServiceCollectionExtensions.cs` |

**Evite:** referenciar `DbContext` ou `HttpContext` aqui.

---

## Domain — o modelo central

**O que é:** definição das entidades e contratos (interfaces) que o resto do sistema usa.

**O que faz:**

- Define `Documento`, `DocumentoAnexo`, `UsuarioSistema`, etc.
- Define `IGenericRepository<T>`, `IDocumentoAnexoRepository`, etc.
- Contém `BaseEntity` (`Id`, datas, `Ativo`)

**Onde mexer quando quiser:**

| Objetivo | Onde |
|----------|------|
| Nova propriedade numa entidade | `Entities/Legacy/` |
| Novo contrato de repositório | `Interfaces/` |
| Read model para consultas | `ReadModels/` |

**Evite:** dependências de EF, ASP.NET ou ficheiros.

---

## Infrastructure — ligação ao mundo real

**O que é:** implementação concreta de persistência, segurança e ficheiros.

**O que faz:**

- EF Core + SQL Server
- Repositórios e migrations
- Geração JWT, hash BCrypt
- Gravação de ficheiros em `storage/documentos`
- Seed de dados de teste

**Onde mexer quando quiser:**

| Objetivo | Onde |
|----------|------|
| Consulta SQL/EF específica | `Data/Repositories/` |
| DbContext / mapeamentos | `Data/Context/` |
| Nova migration | `Data/Migrations/` (com cuidado — ver doc de BD) |
| Storage de ficheiros | `Storage/LocalFileStorageService.cs` |
| Seed de utilizadores | `Security/IdentityDataSeeder.cs` |
| Registar implementações | `DependencyInjection/InfrastructureServiceCollectionExtensions.cs` |

---

## Shared — código comum

**O que é:** pequenas peças usadas em vários projetos.

**Contém:**

- `AppRoles`, `AppPolicies`
- `JwtSettings`, `StorageSettings`
- `ErrorResponse` para erros JSON

**Onde mexer quando quiser:**

| Objetivo | Onde |
|----------|------|
| Nova role ou policy (nome) | `Shared/Security/` |
| Novo setting de configuração | `Shared/Settings/` + registo em `Program.cs` |

---

## Guia rápido: “Quero alterar X”

| Quero alterar… | Camada | Pasta típica |
|----------------|--------|--------------|
| URL ou policy de um endpoint | Api | `Controllers/` |
| Regra “documento aprovado não pode ser encaminhado” | Application | `Services/TramitacaoDocumentoService.cs` |
| Campo numa tabela | Domain + Infrastructure | Entidade + migration |
| Filtro “só anexos ativos” | Infrastructure | `Repositories/DocumentoAnexoRepository.cs` |
| Tamanho máximo de upload | Shared + Application | `StorageSettings` + `DocumentoFileValidator` |
| Tempo de expiração do JWT | Shared + Api | `JwtSettings` (config) + `Program.cs` |
| Onde ficam os PDFs | Infrastructure | `LocalFileStorageService` → pasta `storage/documentos` |

---

## Ciclo saudável de alteração

1. **Entenda** o fluxo: Controller → Service → Repository
2. **Altere** a camada correta (regra no Application, dados no Infrastructure)
3. **Compile** com `dotnet build`
4. **Teste** no Swagger com utilizador adequado (admin/operador/consulta)
5. **Documente** se a alteração for visível para outros developers

Para detalhes sobre Git e uso de IA, consulte [Guia para desenvolvedor iniciante](../guias/guia-para-desenvolvedor-iniciante.md).
