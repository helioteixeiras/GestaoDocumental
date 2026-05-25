# Como executar localmente

## Requisitos

| Requisito | Versão / nota |
|-----------|---------------|
| .NET SDK | **9.0** |
| SQL Server | LocalDB, Express ou instância acessível |
| IDE (opcional) | Visual Studio 2022 ou VS Code |
| SO | Windows (ambiente actual do projecto) |

Ferramenta EF Core CLI (para migrations):

```powershell
dotnet tool install --global dotnet-ef
```

---

## Estrutura relevante

```
GestaoDocumental/
├── GestaoDocumental.sln
├── backend/
│   └── GestaoDocumental.Api/          ← projecto de arranque
├── docs/
└── storage/documentos/                ← criada automaticamente no upload
```

---

## Configuração da base de dados

A connection string está em `GestaoDocumental.Api/appsettings.json` (secção `ConnectionStrings:DefaultConnection`).

**Importante:** este guia não altera configuração. Garanta que aponta para uma instância SQL Server válida antes de executar.

Se for primeira execução numa BD nova que partilha o legado, aplique migrations:

```powershell
cd backend\GestaoDocumental.Infrastructure

dotnet ef database update `
  --startup-project ..\GestaoDocumental.Api\GestaoDocumental.Api.csproj
```

Numa BD legada já alinhada, o EF regista migrations pendentes sem recriar schema (desde que baseline esteja correcta).

---

## Compilar

Na raiz da solution:

```powershell
cd "c:\Users\user\Documents\Visual Studio 2022\2026\GestaoDocumental"

dotnet build GestaoDocumental.sln
```

Corrigir erros de compilação antes de `dotnet run`.

---

## Executar a API

### Perfil HTTP (recomendado para desenvolvimento)

```powershell
dotnet run --project backend\GestaoDocumental.Api\GestaoDocumental.Api.csproj --launch-profile http
```

URL: **http://localhost:5171**

### Perfil HTTPS

```powershell
dotnet run --project backend\GestaoDocumental.Api\GestaoDocumental.Api.csproj --launch-profile https
```

URLs: `https://localhost:7244` e `http://localhost:5171`

---

## Swagger

Com `ASPNETCORE_ENVIRONMENT=Development`:

- Swagger UI: http://localhost:5171/swagger
- OpenAPI JSON: http://localhost:5171/swagger/v1/swagger.json

---

## Login admin (teste rápido)

1. Abrir Swagger
2. `POST /api/Auth/login`:

```json
{
  "username": "admin",
  "password": "Admin123*"
}
```

3. Copiar `token` da resposta
4. **Authorize** → `Bearer {token}`
5. Testar ex.: `GET /api/Dashboard/documentos/resumo`

Outros utilizadores: `operador` / `Operador123*`, `consulta` / `Consulta123*`

---

## O que acontece no arranque

1. Validação AutoMapper
2. **Seed** (`IdentityDataSeeder`) — perfis, utilizadores de teste, dados mínimos
3. Middleware de excepções activo
4. JWT + policies configurados

---

## Problemas comuns

### Processo bloqueando DLL / falha ao compilar

**Sintoma:** `Could not copy ... GestaoDocumental.Api.dll because it is being used by another process`

**Causa:** instância anterior da API ainda a correr.

**Solução:**

```powershell
# Parar processo na porta 5171
Get-NetTCPConnection -LocalPort 5171 -ErrorAction SilentlyContinue |
  Select-Object -ExpandProperty OwningProcess -Unique |
  ForEach-Object { Stop-Process -Id $_ -Force }

# Ou parar pelo nome
Stop-Process -Name "GestaoDocumental.Api" -Force -ErrorAction SilentlyContinue
```

Depois voltar a `dotnet build` e `dotnet run`.

### Erro de ligação SQL

**Sintoma:** timeout ou login failed ao arrancar.

**Verificar:**

- SQL Server a correr
- Connection string correcta
- Permissões do utilizador SQL
- Firewall (instâncias remotas)

### 401 em todos os endpoints

- Token expirado → novo login
- Header incorrecto → `Bearer {token}` (com espaço)
- Relógio do sistema muito desfasado (`ClockSkew = Zero`)

### 403 com token válido

- Role insuficiente (ex.: `consulta` a chamar endpoint `PodeGerirDocumentos`)
- Usar utilizador adequado ao teste

### AutoMapper license warning

Aviso Lucky Penny em Development — não bloqueia execução local.

### Pasta storage

Uploads gravam em `storage/documentos` relativo à raiz da API. Se ficheiro não existir no disco, download devolve 404 mesmo com registo na BD.

---

## Parar a API

`Ctrl+C` no terminal, ou terminar processo na porta 5171 (ver acima).

---

## Próximo passo

Com a API estável, consulte [Endpoints principais](../api/endpoints-principais.md) e [Roadmap](../roadmap/proximos-passos.md) para o frontend Blazor.
