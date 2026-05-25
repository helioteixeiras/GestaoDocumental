# Endpoints principais

Base URL local (perfil `http`): `http://localhost:5171`

Todos os endpoints abaixo (excepto login) requerem header `Authorization: Bearer {token}`.

Legenda de policies:

| Policy | Roles |
|--------|-------|
| `PodeConsultarDocumentos` | Administrador, Operador, Consulta |
| `PodeGerirDocumentos` | Administrador, Operador |
| `PodeAprovarDocumentos` | Administrador, Operador |
| `PodeGerirUsuarios` | Administrador |
| `[Authorize]` | Qualquer utilizador autenticado (sem filtro de role) |

---

## Auth

| Método | URL | Auth | Descrição |
|--------|-----|------|-----------|
| POST | `/api/Auth/login` | Público | Login; devolve JWT |

---

## Documento

| Método | URL | Policy | Descrição |
|--------|-----|--------|-----------|
| GET | `/api/Documento` | PodeConsultarDocumentos | Listar documentos |
| GET | `/api/Documento/{id}` | PodeConsultarDocumentos | Detalhe |
| POST | `/api/Documento` | PodeGerirDocumentos | Criar |
| PUT | `/api/Documento/{id}` | PodeGerirDocumentos | Actualizar |
| DELETE | `/api/Documento/{id}` | PodeGerirDocumentos | Eliminar |
| GET | `/api/Documento/{id}/workflow` | PodeConsultarDocumentos | Timeline e resumo de workflow |

---

## Workflow (TramitacaoDocumento)

| Método | URL | Policy | Descrição |
|--------|-----|--------|-----------|
| POST | `/api/TramitacaoDocumento/{documentoId}/aprovar` | PodeAprovarDocumentos | Aprovar documento |
| POST | `/api/TramitacaoDocumento/{documentoId}/rejeitar` | PodeAprovarDocumentos | Rejeitar documento |
| POST | `/api/TramitacaoDocumento/{documentoId}/encaminhar` | PodeGerirDocumentos | Encaminhar documento |

### CRUD genérico de tramitação (legado/admin)

| Método | URL | Policy | Nota |
|--------|-----|--------|------|
| GET | `/api/TramitacaoDocumento` | [Authorize] | Lista todas |
| GET | `/api/TramitacaoDocumento/{id}` | [Authorize] | Por id |
| POST | `/api/TramitacaoDocumento` | [Authorize] | Criar registo directo |
| PUT | `/api/TramitacaoDocumento/{id}` | [Authorize] | Actualizar |
| DELETE | `/api/TramitacaoDocumento/{id}` | [Authorize] | Eliminar |

**Limitação:** estes endpoints CRUD não usam policies por role — qualquer utilizador autenticado pode aceder.

---

## Upload / Download

| Método | URL | Policy | Descrição |
|--------|-----|--------|-----------|
| POST | `/api/Documento/{id}/upload` | PodeGerirDocumentos | Upload multipart (`file`) |
| GET | `/api/Documento/{id}/download` | PodeConsultarDocumentos | Download última versão activa |
| GET | `/api/Documento/{id}/anexos/{anexoId}/download` | PodeConsultarDocumentos | Download versão específica |

---

## Anexos

| Método | URL | Policy | Descrição |
|--------|-----|--------|-----------|
| GET | `/api/Documento/{id}/anexos` | PodeConsultarDocumentos | Listar anexos activos com versão |
| DELETE | `/api/Documento/{id}/anexos/{anexoId}` | PodeGerirDocumentos | Soft delete |

### CRUD genérico DocumentoAnexo

| Método | URL | Policy |
|--------|-----|--------|
| GET/POST/PUT/DELETE | `/api/DocumentoAnexo` | [Authorize] |

Preferir endpoints em `DocumentoController` para operações de negócio.

---

## Relatórios de downloads

| Método | URL | Policy | Descrição |
|--------|-----|--------|-----------|
| GET | `/api/Documento/{id}/downloads` | PodeConsultarDocumentos | Relatório paginado |
| GET | `/api/Documento/{id}/downloads/resumo` | PodeConsultarDocumentos | Resumo agregado |
| GET | `/api/Documento/{id}/downloads/export/csv` | PodeConsultarDocumentos | Exportação CSV |

Query params comuns: `dataInicio`, `dataFim`, `usuarioId`, `acao`, `page`, `pageSize`.

---

## Dashboard

| Método | URL | Policy | Descrição |
|--------|-----|--------|-----------|
| GET | `/api/Dashboard/documentos/resumo` | PodeConsultarDocumentos | Indicadores para frontend Blazor |

Indicadores incluem: totais por estado, anexos activos, downloads, documentos últimos 30 dias, listas recentes (documentos, downloads, eventos workflow).

---

## Utilizadores e perfis

| Controller | Policy base | Operações |
|------------|-------------|-----------|
| `UsuarioSistemaController` | PodeGerirUsuarios | CRUD `/api/UsuarioSistema` |
| `PerfilController` | PodeGerirUsuarios | CRUD `/api/Perfil` |

---

## Entidades de referência (CRUD genérico)

Todos com **`[Authorize]`** apenas (qualquer role autenticada):

| Controller | Rota base |
|------------|-----------|
| ClassificacaoDocumento | `/api/ClassificacaoDocumento` |
| Colaborador | `/api/Colaborador` |
| Departamento | `/api/Departamento` |
| Direcao | `/api/Direcao` |
| DocumentoHistorico | `/api/DocumentoHistorico` |
| EstadoColaborador | `/api/EstadoColaborador` |
| EstadoDocumento | `/api/EstadoDocumento` |
| EstadoLogin | `/api/EstadoLogin` |
| Fornecedor | `/api/Fornecedor` |
| Genero | `/api/Genero` |
| Municipio | `/api/Municipio` |
| Pais | `/api/Pais` |
| PostoTrabalho | `/api/PostoTrabalho` |
| Provincia | `/api/Provincia` |
| TipoDocumento | `/api/TipoDocumento` |
| TipoDocumentoColaborador | `/api/TipoDocumentoColaborador` |

Padrão por controller: `GET`, `GET/{id}`, `POST`, `PUT/{id}`, `DELETE/{id}`.

**Limitação:** estes CRUDs não têm policies granulares — evolução futura deve restringir escrita a Administrador.

---

## Códigos HTTP comuns

| Código | Origem típica |
|--------|---------------|
| 200 | Sucesso |
| 201 | Criação (POST) |
| 204 | Actualização/remoção sem corpo |
| 400 | Validação ou regra de negócio (`ValidationException`, `InvalidOperationException`) |
| 401 | Não autenticado / credenciais inválidas |
| 403 | Autenticado sem role adequada |
| 404 | Recurso não encontrado (`KeyNotFoundException` ou `NotFound()` do controller) |
| 500 | Erro inesperado (detalhe só em Development) |

Respostas de erro JSON via `GlobalExceptionMiddleware` e `ErrorResponse`.

---

## Swagger

Disponível em Development:

- UI: `http://localhost:5171/swagger`
- JSON: `http://localhost:5171/swagger/v1/swagger.json`

Autenticação Bearer configurada — ver [JWT, roles e policies](../seguranca/jwt-roles-policies.md).
