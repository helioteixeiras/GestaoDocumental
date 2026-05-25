$base = "c:\Users\user\Documents\Visual Studio 2022\2026\GestaoDocumental\backend\GestaoDocumental.Api\DTOs"

function New-DtoFile {
    param(
        [string]$Entity,
        [string]$DtoName,
        [string[]]$Properties
    )

    $dir = Join-Path $base $Entity
    New-Item -ItemType Directory -Force -Path $dir | Out-Null

    $ns = "GestaoDocumental.Api.DTOs.$Entity"
    $body = ($Properties -join "`r`n`r`n    ")

    $content = @"
namespace $ns;

public class ${Entity}${DtoName}Dto
{
    $body
}
"@

    $path = Join-Path $dir "${Entity}${DtoName}Dto.cs"
    Set-Content -Path $path -Value $content -Encoding UTF8
    Write-Output "Created ${Entity}${DtoName}Dto.cs"
}

# 1. ClassificacaoDocumento
New-DtoFile "ClassificacaoDocumento" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "ClassificacaoDocumento" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "ClassificacaoDocumento" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "ClassificacaoDocumento" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 2. Colaborador
New-DtoFile "Colaborador" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string NumDocumento { get; set; } = string.Empty;",
    "public string? Email { get; set; }",
    "public string? Cargo { get; set; }",
    "public int EstadoId { get; set; }",
    "public int PerfilId { get; set; }",
    "public int PostoTrabalhoId { get; set; }"
)
New-DtoFile "Colaborador" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public int TipoDocumentoColaboradorId { get; set; }",
    "public string NumDocumento { get; set; } = string.Empty;",
    "public string? NumMecanografo { get; set; }",
    "public DateTime? DataNascimento { get; set; }",
    "public string? Endereco { get; set; }",
    "public int? NacionalidadeId { get; set; }",
    "public string? Email { get; set; }",
    "public int PostoTrabalhoId { get; set; }",
    "public int? GeneroId { get; set; }",
    "public string? Cargo { get; set; }",
    "public int EstadoId { get; set; }",
    "public int PerfilId { get; set; }"
)
$colabEditable = @(
    "public string Nome { get; set; } = string.Empty;",
    "public int TipoDocumentoColaboradorId { get; set; }",
    "public string NumDocumento { get; set; } = string.Empty;",
    "public string? NumMecanografo { get; set; }",
    "public DateTime? DataNascimento { get; set; }",
    "public string? Endereco { get; set; }",
    "public int? NacionalidadeId { get; set; }",
    "public string? Email { get; set; }",
    "public int PostoTrabalhoId { get; set; }",
    "public int? GeneroId { get; set; }",
    "public string? Cargo { get; set; }",
    "public int EstadoId { get; set; }",
    "public int PerfilId { get; set; }"
)
New-DtoFile "Colaborador" "Create" $colabEditable
New-DtoFile "Colaborador" "Update" $colabEditable

# 3. Departamento
New-DtoFile "Departamento" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DirecaoId { get; set; }"
)
New-DtoFile "Departamento" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DirecaoId { get; set; }"
)
New-DtoFile "Departamento" "Create" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DirecaoId { get; set; }"
)
New-DtoFile "Departamento" "Update" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DirecaoId { get; set; }"
)

# 4. Direcao
New-DtoFile "Direcao" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }"
)
New-DtoFile "Direcao" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }"
)
New-DtoFile "Direcao" "Create" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }"
)
New-DtoFile "Direcao" "Update" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }"
)

# 5. Documento
New-DtoFile "Documento" "List" @(
    "public int Id { get; set; }",
    "public string NumeroDocumento { get; set; } = string.Empty;",
    "public string Titulo { get; set; } = string.Empty;",
    "public int TipoDocumentoId { get; set; }",
    "public int ClassificacaoId { get; set; }",
    "public int EstadoDocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public DateTime DataCriacao { get; set; }",
    "public DateTime? DataDocumento { get; set; }"
)
New-DtoFile "Documento" "Details" @(
    "public int Id { get; set; }",
    "public string NumeroDocumento { get; set; } = string.Empty;",
    "public string Titulo { get; set; } = string.Empty;",
    "public string? Descricao { get; set; }",
    "public int TipoDocumentoId { get; set; }",
    "public int ClassificacaoId { get; set; }",
    "public int EstadoDocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public int ColaboradorCriadorId { get; set; }",
    "public int? FornecedorId { get; set; }",
    "public DateTime DataCriacao { get; set; }",
    "public DateTime? DataDocumento { get; set; }",
    "public DateTime? DataRecepcao { get; set; }",
    "public DateTime? PrazoResposta { get; set; }",
    "public string? ReferenciaExterna { get; set; }",
    "public string? PalavrasChave { get; set; }",
    "public string? Observacao { get; set; }",
    "public int? VersaoAtual { get; set; }",
    "public string? LocalizacaoFisica { get; set; }",
    "public string? CodigoArquivo { get; set; }",
    "public DateTime? DataAtualizacao { get; set; }",
    "public int? UtilizadorAtualizacaoId { get; set; }"
)
New-DtoFile "Documento" "Create" @(
    "public string NumeroDocumento { get; set; } = string.Empty;",
    "public string Titulo { get; set; } = string.Empty;",
    "public string? Descricao { get; set; }",
    "public int TipoDocumentoId { get; set; }",
    "public int ClassificacaoId { get; set; }",
    "public int EstadoDocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public int ColaboradorCriadorId { get; set; }",
    "public int? FornecedorId { get; set; }",
    "public DateTime? DataDocumento { get; set; }",
    "public DateTime? DataRecepcao { get; set; }",
    "public DateTime? PrazoResposta { get; set; }",
    "public string? ReferenciaExterna { get; set; }",
    "public string? PalavrasChave { get; set; }",
    "public string? Observacao { get; set; }",
    "public string? LocalizacaoFisica { get; set; }",
    "public string? CodigoArquivo { get; set; }"
)
New-DtoFile "Documento" "Update" @(
    "public string NumeroDocumento { get; set; } = string.Empty;",
    "public string Titulo { get; set; } = string.Empty;",
    "public string? Descricao { get; set; }",
    "public int TipoDocumentoId { get; set; }",
    "public int ClassificacaoId { get; set; }",
    "public int EstadoDocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public int? FornecedorId { get; set; }",
    "public DateTime? DataDocumento { get; set; }",
    "public DateTime? DataRecepcao { get; set; }",
    "public DateTime? PrazoResposta { get; set; }",
    "public string? ReferenciaExterna { get; set; }",
    "public string? PalavrasChave { get; set; }",
    "public string? Observacao { get; set; }",
    "public string? LocalizacaoFisica { get; set; }",
    "public string? CodigoArquivo { get; set; }"
)

# 6. DocumentoAnexo
New-DtoFile "DocumentoAnexo" "List" @(
    "public int Id { get; set; }",
    "public int DocumentoId { get; set; }",
    "public string NomeOriginal { get; set; } = string.Empty;",
    "public string? Extensao { get; set; }",
    "public long? Tamanho { get; set; }",
    "public DateTime DataUpload { get; set; }"
)
New-DtoFile "DocumentoAnexo" "Details" @(
    "public int Id { get; set; }",
    "public int DocumentoId { get; set; }",
    "public Guid GuidFicheiro { get; set; }",
    "public string NomeOriginal { get; set; } = string.Empty;",
    "public string? Extensao { get; set; }",
    "public long? Tamanho { get; set; }",
    "public DateTime DataUpload { get; set; }"
)
New-DtoFile "DocumentoAnexo" "Create" @(
    "public int DocumentoId { get; set; }",
    "public string NomeOriginal { get; set; } = string.Empty;",
    "public string? Extensao { get; set; }",
    "public long? Tamanho { get; set; }"
)
New-DtoFile "DocumentoAnexo" "Update" @(
    "public string NomeOriginal { get; set; } = string.Empty;",
    "public string? Extensao { get; set; }"
)

# 7. DocumentoHistorico
New-DtoFile "DocumentoHistorico" "List" @(
    "public int Id { get; set; }",
    "public int DocumentoId { get; set; }",
    "public int UtilizadorId { get; set; }",
    "public string Acao { get; set; } = string.Empty;",
    "public DateTime DataAcao { get; set; }"
)
New-DtoFile "DocumentoHistorico" "Details" @(
    "public int Id { get; set; }",
    "public int DocumentoId { get; set; }",
    "public int UtilizadorId { get; set; }",
    "public string Acao { get; set; } = string.Empty;",
    "public string? Observacao { get; set; }",
    "public DateTime DataAcao { get; set; }"
)
New-DtoFile "DocumentoHistorico" "Create" @(
    "public int DocumentoId { get; set; }",
    "public string Acao { get; set; } = string.Empty;",
    "public string? Observacao { get; set; }"
)
New-DtoFile "DocumentoHistorico" "Update" @(
    "public string Acao { get; set; } = string.Empty;",
    "public string? Observacao { get; set; }"
)

# 8. EstadoColaborador
New-DtoFile "EstadoColaborador" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoColaborador" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoColaborador" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoColaborador" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 9. EstadoDocumento
New-DtoFile "EstadoDocumento" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoDocumento" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoDocumento" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoDocumento" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 10. EstadoLogin
New-DtoFile "EstadoLogin" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoLogin" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoLogin" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "EstadoLogin" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 11. Fornecedor
New-DtoFile "Fornecedor" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Nif { get; set; }",
    "public string? ContactoPrincipal { get; set; }",
    "public string? Email1 { get; set; }"
)
New-DtoFile "Fornecedor" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Nif { get; set; }",
    "public string? Endereco { get; set; }",
    "public string? ContactoPrincipal { get; set; }",
    "public string? ContactoAlternativo { get; set; }",
    "public string? Email1 { get; set; }",
    "public string? Email2 { get; set; }",
    "public string? PontoFocal { get; set; }",
    "public string? Notas { get; set; }"
)
New-DtoFile "Fornecedor" "Create" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Nif { get; set; }",
    "public string? Endereco { get; set; }",
    "public string? ContactoPrincipal { get; set; }",
    "public string? ContactoAlternativo { get; set; }",
    "public string? Email1 { get; set; }",
    "public string? Email2 { get; set; }",
    "public string? PontoFocal { get; set; }",
    "public string? Notas { get; set; }"
)
New-DtoFile "Fornecedor" "Update" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Nif { get; set; }",
    "public string? Endereco { get; set; }",
    "public string? ContactoPrincipal { get; set; }",
    "public string? ContactoAlternativo { get; set; }",
    "public string? Email1 { get; set; }",
    "public string? Email2 { get; set; }",
    "public string? PontoFocal { get; set; }",
    "public string? Notas { get; set; }"
)

# 12. Genero
New-DtoFile "Genero" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Genero" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Genero" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Genero" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 13. Municipio
New-DtoFile "Municipio" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public int ProvinciaId { get; set; }"
)
New-DtoFile "Municipio" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public int ProvinciaId { get; set; }"
)
New-DtoFile "Municipio" "Create" @(
    "public string Nome { get; set; } = string.Empty;",
    "public int ProvinciaId { get; set; }"
)
New-DtoFile "Municipio" "Update" @(
    "public string Nome { get; set; } = string.Empty;",
    "public int ProvinciaId { get; set; }"
)

# 14. Pais
New-DtoFile "Pais" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Pais" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Pais" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Pais" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 15. Perfil
New-DtoFile "Perfil" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Perfil" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Perfil" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "Perfil" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 16. PostoTrabalho
New-DtoFile "PostoTrabalho" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DepartamentoId { get; set; }",
    "public int MunicipioId { get; set; }"
)
New-DtoFile "PostoTrabalho" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DepartamentoId { get; set; }",
    "public int MunicipioId { get; set; }"
)
New-DtoFile "PostoTrabalho" "Create" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DepartamentoId { get; set; }",
    "public int MunicipioId { get; set; }"
)
New-DtoFile "PostoTrabalho" "Update" @(
    "public string Nome { get; set; } = string.Empty;",
    "public string? Sigla { get; set; }",
    "public int DepartamentoId { get; set; }",
    "public int MunicipioId { get; set; }"
)

# 17. Provincia
New-DtoFile "Provincia" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public int PaisId { get; set; }"
)
New-DtoFile "Provincia" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;",
    "public int PaisId { get; set; }"
)
New-DtoFile "Provincia" "Create" @(
    "public string Nome { get; set; } = string.Empty;",
    "public int PaisId { get; set; }"
)
New-DtoFile "Provincia" "Update" @(
    "public string Nome { get; set; } = string.Empty;",
    "public int PaisId { get; set; }"
)

# 18. TipoDocumento
New-DtoFile "TipoDocumento" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "TipoDocumento" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "TipoDocumento" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "TipoDocumento" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 19. TipoDocumentoColaborador
New-DtoFile "TipoDocumentoColaborador" "List" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "TipoDocumentoColaborador" "Details" @(
    "public int Id { get; set; }",
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "TipoDocumentoColaborador" "Create" @(
    "public string Nome { get; set; } = string.Empty;"
)
New-DtoFile "TipoDocumentoColaborador" "Update" @(
    "public string Nome { get; set; } = string.Empty;"
)

# 20. TramitacaoDocumento
New-DtoFile "TramitacaoDocumento" "List" @(
    "public int Id { get; set; }",
    "public int DocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public int DirecaoDestinoId { get; set; }",
    "public string? Estado { get; set; }",
    "public DateTime DataEnvio { get; set; }",
    "public DateTime? DataRececao { get; set; }"
)
New-DtoFile "TramitacaoDocumento" "Details" @(
    "public int Id { get; set; }",
    "public int DocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public int DirecaoDestinoId { get; set; }",
    "public int? ColaboradorOrigemId { get; set; }",
    "public int? ColaboradorDestinoId { get; set; }",
    "public string? Estado { get; set; }",
    "public string? Observacao { get; set; }",
    "public DateTime DataEnvio { get; set; }",
    "public DateTime? DataRececao { get; set; }"
)
New-DtoFile "TramitacaoDocumento" "Create" @(
    "public int DocumentoId { get; set; }",
    "public int DirecaoOrigemId { get; set; }",
    "public int DirecaoDestinoId { get; set; }",
    "public int? ColaboradorOrigemId { get; set; }",
    "public int? ColaboradorDestinoId { get; set; }",
    "public string? Estado { get; set; }",
    "public string? Observacao { get; set; }"
)
New-DtoFile "TramitacaoDocumento" "Update" @(
    "public int DirecaoDestinoId { get; set; }",
    "public int? ColaboradorDestinoId { get; set; }",
    "public string? Estado { get; set; }",
    "public string? Observacao { get; set; }",
    "public DateTime? DataRececao { get; set; }"
)

# 21. UsuarioSistema
New-DtoFile "UsuarioSistema" "List" @(
    "public int Id { get; set; }",
    "public int ColaboradorId { get; set; }",
    "public string Username { get; set; } = string.Empty;",
    "public int EstadoLoginId { get; set; }",
    "public bool? Ativo { get; set; }",
    "public bool? Bloqueado { get; set; }"
)
New-DtoFile "UsuarioSistema" "Details" @(
    "public int Id { get; set; }",
    "public int ColaboradorId { get; set; }",
    "public string Username { get; set; } = string.Empty;",
    "public int EstadoLoginId { get; set; }",
    "public bool? Ativo { get; set; }",
    "public bool? Bloqueado { get; set; }",
    "public DateTime? UltimoLogin { get; set; }",
    "public DateTime DataCriacao { get; set; }"
)
New-DtoFile "UsuarioSistema" "Create" @(
    "public int ColaboradorId { get; set; }",
    "public string Username { get; set; } = string.Empty;",
    "public int EstadoLoginId { get; set; }",
    "public bool? Ativo { get; set; }"
)
New-DtoFile "UsuarioSistema" "Update" @(
    "public string Username { get; set; } = string.Empty;",
    "public int EstadoLoginId { get; set; }",
    "public bool? Ativo { get; set; }",
    "public bool? Bloqueado { get; set; }"
)

$fileCount = (Get-ChildItem -Path $base -Recurse -Filter "*.cs" | Measure-Object).Count
Write-Output ""
Write-Output "Concluido: $fileCount ficheiros em $base"
