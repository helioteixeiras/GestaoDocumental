$ErrorActionPreference = "Stop"

$migrationsDir = "c:\Users\user\Documents\Visual Studio 2022\2026\GestaoDocumental\backend\GestaoDocumental.Infrastructure\Data\Migrations"
$sourcePath = Join-Path $migrationsDir "20260525141826_TempFullModel.Designer.cs"
$content = Get-Content $sourcePath -Raw

function Remove-PropertyLines {
    param(
        [string]$Text,
        [string[]]$PropertyNames
    )

    foreach ($name in $PropertyNames) {
        $Text = [regex]::Replace(
            $Text,
            "(?m)^\s*b\.Property<[^>]+>\(""$name""\)[\s\S]*?;\r?\n",
            ""
        )
    }

    return $Text
}

function Remove-HasIndexLines {
    param(
        [string]$Text,
        [string[]]$IndexPatterns
    )

    foreach ($pattern in $IndexPatterns) {
        $Text = [regex]::Replace(
            $Text,
            "(?m)^\s*b\.HasIndex\($pattern\)[\s\S]*?;\r?\n",
            ""
        )
    }

    return $Text
}

$entities = @(
    "ClassificacaoDocumento",
    "Colaborador",
    "Departamento",
    "Direcao",
    "DocumentoAnexo",
    "DocumentoHistorico",
    "EstadoColaborador",
    "EstadoDocumento",
    "EstadoLogin",
    "Fornecedor",
    "Genero",
    "Municipio",
    "Pais",
    "Perfil",
    "PostoTrabalho",
    "Provincia",
    "TipoDocumento",
    "TipoDocumentoColaborador",
    "TramitacaoDocumento"
)

foreach ($entity in $entities) {
    $pattern = "(?ms)(modelBuilder\.Entity\(""GestaoDocumental\.Domain\.Entities\.Legacy\.$entity""[^=]*=>\s*\{)(.*?)(^\s*\}\);)"
    $content = [regex]::Replace($content, $pattern, {
        param($match)
        $header = $match.Groups[1].Value
        $body = $match.Groups[2].Value
        $footer = $match.Groups[3].Value

        $body = Remove-PropertyLines $body @("Ativo", "DataCriacao", "DataAtualizacao")

        return $header + $body + $footer
    })
}

$content = [regex]::Replace(
    $content,
    "(?ms)(modelBuilder\.Entity\(""GestaoDocumental\.Domain\.Entities\.Legacy\.Documento""[^=]*=>\s*\{)(.*?)(^\s*\}\);)",
    {
        param($match)
        $header = $match.Groups[1].Value
        $body = Remove-PropertyLines $match.Groups[2].Value @("Ativo")
        return $header + $body + $match.Groups[3].Value
    }
)

$content = [regex]::Replace(
    $content,
    "(?ms)(modelBuilder\.Entity\(""GestaoDocumental\.Domain\.Entities\.Legacy\.UsuarioSistema""[^=]*=>\s*\{)(.*?)(^\s*\}\);)",
    {
        param($match)
        $header = $match.Groups[1].Value
        $body = Remove-PropertyLines $match.Groups[2].Value @("DataAtualizacao", "Email", "PerfilId")
        $body = Remove-HasIndexLines $body @('new\[\] \{ "Email" \}', '"PerfilId"')
        return $header + $body + $match.Groups[3].Value
    },
    1
)

$content = [regex]::Replace(
    $content,
    "(?ms)(modelBuilder\.Entity\(""GestaoDocumental\.Domain\.Entities\.Legacy\.UsuarioSistema""[^=]*=>\s*\{)(.*?)(^\s*\}\);)",
    {
        param($match)
        $header = $match.Groups[1].Value
        $body = $match.Groups[2].Value
        $footer = $match.Groups[3].Value

        $body = [regex]::Replace(
            $body,
            "(?ms)\r?\n\s*b\.HasOne\(""GestaoDocumental\.Domain\.Entities\.Legacy\.Perfil"", ""Perfil""\)[\s\S]*?;\r?\n",
            "`r`n"
        )
        $body = [regex]::Replace($body, "\r?\n\s*b\.Navigation\(""Perfil""\);\r?\n", "`r`n")

        return $header + $body + $footer
    },
    1
)

$content = [regex]::Replace(
    $content,
    "(?ms)(modelBuilder\.Entity\(""GestaoDocumental\.Domain\.Entities\.Legacy\.Perfil""[^=]*=>\s*\{)(.*?)(^\s*\}\);)",
    {
        param($match)
        $header = $match.Groups[1].Value
        $body = $match.Groups[2].Value
        $footer = $match.Groups[3].Value

        if ($body -match 'b\.Navigation\("Colaboradors"\)') {
            $body = [regex]::Replace($body, "\r?\n\s*b\.Navigation\(""UsuarioSistemas""\);\r?\n", "`r`n")
        }

        return $header + $body + $footer
    },
    1
)

$content = $content -replace '\[Migration\("20260525141826_TempFullModel"\)\]', '[Migration("20260525135510_InitialBaseline")]'
$content = $content -replace 'partial class TempFullModel', 'partial class InitialBaseline'

$baselineDesignerPath = Join-Path $migrationsDir "20260525135510_InitialBaseline.Designer.cs"
Set-Content -Path $baselineDesignerPath -Value $content -Encoding UTF8
Write-Output "Created $baselineDesignerPath"
