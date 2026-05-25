# JWT, roles e policies

## Visão geral

A API protege endpoints com **JWT Bearer**. O cliente envia o token no header:

```
Authorization: Bearer {token}
```

A configuração está em `Program.cs` e usa settings da secção `JwtSettings` (Issuer, Audience, SecretKey, ExpiryMinutes).

---

## Login

### Endpoint

```
POST /api/Auth/login
```

Público (`[AllowAnonymous]`). Não requer token.

### Corpo do pedido

```json
{
  "username": "admin",
  "password": "Admin123*"
}
```

### Resposta (sucesso)

```json
{
  "token": "...",
  "expiresAt": "2026-05-25T22:00:00Z",
  "username": "admin",
  "email": "admin@gestaodocumental.local",
  "role": "Administrador"
}
```

### Fluxo interno

1. `AuthController` → `AuthService.LoginAsync`
2. Procura `UsuarioSistema` por username
3. Valida: utilizador existe, `Ativo = true`, `Bloqueado = false`
4. Verifica password com **BCrypt** (`PasswordHasher`)
5. Atualiza `UltimoLogin`
6. Gera JWT via `JwtTokenGenerator`

### Erros de login

Credenciais inválidas ou utilizador inativo/bloqueado → `UnauthorizedAccessException` → **HTTP 401** com mensagem genérica `"Invalid username or password."`

**Limitação:** não existe endpoint HTTP público de registo. O método `RegisterAsync` existe no `AuthService`, mas não está exposto num controller.

---

## JWT — o que contém

O token inclui estas claims (tipos padrão .NET):

| Claim | Conteúdo |
|-------|----------|
| `NameIdentifier` | Id do `UsuarioSistema` |
| `Name` | Username |
| `Email` | Email |
| `Role` | Nome do perfil (`Administrador`, `Operador`, `Consulta`) |

Configuração relevante:

- `MapInboundClaims = false` — mantém nomes de claims estáveis
- `RoleClaimType = ClaimTypes.Role` — policies usam a claim de role
- Expiração: `ExpiryMinutes` em `JwtSettings` (default 60 minutos)
- Algoritmo: HMAC-SHA256

---

## Roles

Definidas em `Shared/Security/AppRoles.cs`:

| Role | Descrição funcional |
|------|---------------------|
| **Administrador** | Acesso total; gere utilizadores e documentos |
| **Operador** | Gere documentos, aprova/rejeita, encaminha |
| **Consulta** | Apenas consulta documentos, downloads e dashboard |

Cada utilizador tem um `Perfil` na BD cujo `Nome` coincide com a role no JWT.

---

## Policies

Definidas em `Shared/Security/AppPolicies.cs` e registadas em `Program.cs`:

| Policy | Roles permitidas |
|--------|------------------|
| `AdministradorOnly` | Administrador |
| `PodeGerirUsuarios` | Administrador |
| `PodeGerirDocumentos` | Administrador, Operador |
| `PodeConsultarDocumentos` | Administrador, Operador, Consulta |
| `PodeAprovarDocumentos` | Administrador, Operador |

### Como usar nos controllers

```csharp
[Authorize(Policy = AppPolicies.PodeGerirDocumentos)]
```

Controllers podem ter `[Authorize]` genérico (qualquer utilizador autenticado) e actions com policy mais restritiva.

---

## Utilizadores de teste (seed)

Criados automaticamente no arranque por `IdentityDataSeeder` (se ainda não existirem):

| Username | Password | Role | Notas |
|----------|----------|------|-------|
| `admin` | `Admin123*` | Administrador | `ColaboradorId` null — histórico usa `ColaboradorCriadorId` do documento |
| `operador` | `Operador123*` | Operador | Colaborador associado |
| `consulta` | `Consulta123*` | Consulta | Colaborador associado |

O seed também cria dados de referência mínimos e um documento de teste (`DOC-TEST-AUTH-001`).

---

## Swagger com JWT

Em ambiente **Development**, Swagger UI está activo. Para testar endpoints protegidos:

1. Fazer login em `POST /api/Auth/login`
2. Copiar o `token`
3. Clicar **Authorize** no Swagger
4. Inserir: `Bearer {token}`

---

## 401 vs 403

| Código | Significado | Quando acontece neste projeto |
|--------|-------------|-------------------------------|
| **401 Unauthorized** | Não autenticado ou credenciais inválidas | Sem token, token expirado/inválido, login falhado, claim `NameIdentifier` inválida em operações que exigem utilizador |
| **403 Forbidden** | Autenticado, mas sem permissão | Token válido, mas role não satisfaz a policy do endpoint (ex.: `consulta` num endpoint `PodeGerirDocumentos`) |

Nota: exceções `UnauthorizedAccessException` lançadas **dentro** de services (utilizador inválido após autenticação) são mapeadas pelo middleware para **401**, não 403.

---

## Boas práticas de segurança (estado actual)

- Passwords nunca armazenadas em texto claro (BCrypt)
- Endpoints sensíveis protegidos por policy
- Mensagens de login genéricas (não revelam se o username existe)

**Limitações actuais:**

- Sem refresh token (renovar exige novo login)
- Policies fixas em código, não configuráveis por perfil na BD
- Alguns controllers CRUD genéricos usam apenas `[Authorize]` sem policy por role — ver [Endpoints principais](../api/endpoints-principais.md)
