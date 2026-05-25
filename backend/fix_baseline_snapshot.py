import re
from pathlib import Path

files = [
    Path(r"GestaoDocumental.Infrastructure/Data/Migrations/GestaoDocumentalDbContextModelSnapshot.cs"),
    Path(r"GestaoDocumental.Infrastructure/Data/Migrations/20260525135510_InitialBaseline.Designer.cs"),
]

root = Path(r"c:\Users\user\Documents\Visual Studio 2022\2026\GestaoDocumental\backend")


def strip_entity_properties(content, entity_name, keep=None):
    keep = keep or set()
    pattern = re.compile(
        rf'(modelBuilder\.Entity\("{re.escape(entity_name)}"[\s\S]*?\{{)([\s\S]*?)(\n\s*\}}\);)',
        re.MULTILINE,
    )

    def repl(match):
        header, body, footer = match.groups()
        for prop in ("Ativo", "DataCriacao", "DataAtualizacao"):
            if prop in keep:
                continue
            body = re.sub(
                rf'\n\s*b\.Property<[^>]+>\("{prop}"\)[\s\S]*?;\n',
                "\n",
                body,
            )
        return header + body + footer

    return pattern.sub(repl, content, count=1)


entities = [
    "GestaoDocumental.Domain.Entities.Legacy.ClassificacaoDocumento",
    "GestaoDocumental.Domain.Entities.Legacy.Colaborador",
    "GestaoDocumental.Domain.Entities.Legacy.Departamento",
    "GestaoDocumental.Domain.Entities.Legacy.Direcao",
    "GestaoDocumental.Domain.Entities.Legacy.DocumentoAnexo",
    "GestaoDocumental.Domain.Entities.Legacy.DocumentoHistorico",
    "GestaoDocumental.Domain.Entities.Legacy.EstadoColaborador",
    "GestaoDocumental.Domain.Entities.Legacy.EstadoDocumento",
    "GestaoDocumental.Domain.Entities.Legacy.EstadoLogin",
    "GestaoDocumental.Domain.Entities.Legacy.Fornecedor",
    "GestaoDocumental.Domain.Entities.Legacy.Genero",
    "GestaoDocumental.Domain.Entities.Legacy.Municipio",
    "GestaoDocumental.Domain.Entities.Legacy.Pais",
    "GestaoDocumental.Domain.Entities.Legacy.Perfil",
    "GestaoDocumental.Domain.Entities.Legacy.PostoTrabalho",
    "GestaoDocumental.Domain.Entities.Legacy.Provincia",
    "GestaoDocumental.Domain.Entities.Legacy.TipoDocumento",
    "GestaoDocumental.Domain.Entities.Legacy.TipoDocumentoColaborador",
    "GestaoDocumental.Domain.Entities.Legacy.TramitacaoDocumento",
]

for relative_path in files:
    file_path = root / relative_path
    content = file_path.read_text(encoding="utf-8")

    for entity in entities:
        content = strip_entity_properties(content, entity)

    content = strip_entity_properties(
        content,
        "GestaoDocumental.Domain.Entities.Legacy.Documento",
        keep={"DataCriacao", "DataAtualizacao"},
    )

    content = strip_entity_properties(
        content,
        "GestaoDocumental.Domain.Entities.Legacy.UsuarioSistema",
        keep={"Ativo", "DataCriacao"},
    )

    content = re.sub(
        r'\n\s*b\.Property<string>\("Email"\)[\s\S]*?;\n',
        "\n",
        content,
    )
    content = re.sub(
        r'\n\s*b\.Property<int>\("PerfilId"\)[\s\S]*?;\n',
        "\n",
        content,
    )
    content = re.sub(
        r'\n\s*b\.HasIndex\("PerfilId"\);\n',
        "\n",
        content,
    )
    content = re.sub(
        r'\n\s*b\.HasIndex\(new\[\] \{ "Email" \}, "UQ_UsuarioSistema_Email"\)[\s\S]*?;\n',
        "\n",
        content,
    )
    content = re.sub(
        r'\n\s*b\.HasOne\("GestaoDocumental\.Domain\.Entities\.Legacy\.Perfil", "Perfil"\)[\s\S]*?HasConstraintName\("FK_UsuarioSistema_Perfil"\);\n',
        "\n",
        content,
    )
    content = re.sub(
        r'\n\s*b\.Navigation\("Perfil"\);\n',
        "\n",
        content,
    )
    content = re.sub(
        r'(\n\s*b\.Navigation\("Colaboradors"\);)\n\s*b\.Navigation\("UsuarioSistemas"\);',
        r"\1",
        content,
    )

    file_path.write_text(content, encoding="utf-8")
    print(f"Updated {file_path.name}")
