# Gestão Documental — Documentação do Backend

Documentação técnica, funcional e operacional do backend **GestãoDocumental**, preparada antes do início do frontend Blazor.

## Visão geral

O backend é uma **API REST** em **ASP.NET Core (.NET 9)** que gere documentos, utilizadores, estrutura organizacional e workflow documental. Utiliza **SQL Server**, **Entity Framework Core**, autenticação **JWT** e uma arquitetura em camadas inspirada em **Clean Architecture**.

Principais capacidades já implementadas:

- Autenticação JWT com roles e policies
- CRUD de entidades de referência e documentos
- Workflow documental (aprovar, rejeitar, encaminhar, timeline)
- Upload físico de anexos com versionamento v1
- Soft delete de anexos
- Auditoria e relatórios de downloads (incluindo CSV)
- Dashboard/resumo documental para consumo pelo frontend

## Módulos existentes

| Módulo | Descrição |
|--------|-----------|
| **Auth** | Login JWT, hash BCrypt, seed de utilizadores de teste |
| **Segurança** | Roles, policies, proteção de endpoints |
| **Documentos** | CRUD, workflow, anexos, downloads, relatórios |
| **Tramitação** | Aprovação, rejeição, encaminhamento |
| **Upload/Storage** | Ficheiros locais em `storage/documentos` |
| **Auditoria** | Histórico em `DocumentoHistorico` |
| **Dashboard** | Indicadores agregados para Blazor |
| **Referências** | Colaboradores, direções, tipos, estados, etc. |
| **Infraestrutura** | EF Core, migrations, repositórios, UnitOfWork |

## Arquitetura resumida

```
Cliente (Swagger / futuro Blazor)
        ↓
GestaoDocumental.Api          → Controllers, middleware, Swagger
        ↓
GestaoDocumental.Application  → Services, DTOs, validações, regras
        ↓
GestaoDocumental.Domain       → Entidades, interfaces de repositório
        ↓
GestaoDocumental.Infrastructure → EF Core, repositórios, JWT, storage, seed
        ↓
SQL Server + pasta storage/documentos
```

O projeto **GestaoDocumental.Shared** contém constantes de segurança, settings e respostas comuns, partilhados por várias camadas.

## Documentação interna

### Arquitetura
- [01 — Visão geral](arquitetura/01-visao-geral.md)
- [02 — Camadas em termos simples](arquitetura/02-camadas-em-termos-simples.md)

### Segurança
- [JWT, roles e policies](seguranca/jwt-roles-policies.md)

### Funcional
- [Workflow documental](workflow/workflow-documental.md)
- [Upload, versionamento e auditoria](uploads/upload-versionamento-auditoria.md)
- [Endpoints principais](api/endpoints-principais.md)

### Base de dados e execução
- [Migrations e base legada](database/migrations-e-base-legada.md)
- [Como executar localmente](deploy/como-executar-localmente.md)

### Evolução e onboarding
- [Próximos passos (roadmap)](roadmap/proximos-passos.md)
- [Guia para desenvolvedor iniciante](guias/guia-para-desenvolvedor-iniciante.md)

## Regras importantes do projeto

- **Não apagar nem recriar** a base de dados legada.
- **Não alterar migrations existentes** sem aprovação explícita.
- Preservar JWT, policies, seed e middleware de exceções.
- Novas funcionalidades devem respeitar a separação de camadas.

## Limitações conhecidas (v1)

- Sem refresh token nem endpoint público de registo.
- Policies baseadas em roles fixas (não dinâmicas por perfil na BD).
- Workflow com estados definidos por constantes em código.
- Soft delete de anexos **não remove** o ficheiro físico do disco.
- Versão de anexo calculada em runtime (sem coluna dedicada na BD).
