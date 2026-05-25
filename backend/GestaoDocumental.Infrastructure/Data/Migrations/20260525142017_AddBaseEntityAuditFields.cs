using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDocumental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "UsuarioSistema",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UsuarioSistema",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PerfilId",
                table: "UsuarioSistema",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "TramitacaoDocumento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "TramitacaoDocumento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "TramitacaoDocumento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "TipoDocumentoColaborador",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "TipoDocumentoColaborador",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "TipoDocumentoColaborador",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "TipoDocumento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "TipoDocumento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "TipoDocumento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Provincia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Provincia",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Provincia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PostoTrabalho",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "PostoTrabalho",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PostoTrabalho",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Perfil",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Perfil",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Perfil",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Pais",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Pais",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Pais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Municipio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Municipio",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Municipio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Genero",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Genero",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Genero",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Fornecedor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Fornecedor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Fornecedor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "EstadoLogin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "EstadoLogin",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "EstadoLogin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "EstadoDocumento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "EstadoDocumento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "EstadoDocumento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "EstadoColaborador",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "EstadoColaborador",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "EstadoColaborador",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "DocumentoHistorico",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "DocumentoHistorico",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "DocumentoHistorico",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "DocumentoAnexo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "DocumentoAnexo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "DocumentoAnexo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Documento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Direcao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Direcao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Direcao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Departamento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Departamento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Departamento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Colaborador",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Colaborador",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Colaborador",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "ClassificacaoDocumento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "ClassificacaoDocumento",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "ClassificacaoDocumento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSistema_PerfilId",
                table: "UsuarioSistema",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "UQ_UsuarioSistema_Email",
                table: "UsuarioSistema",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioSistema_Perfil",
                table: "UsuarioSistema",
                column: "PerfilId",
                principalTable: "Perfil",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioSistema_Perfil",
                table: "UsuarioSistema");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioSistema_PerfilId",
                table: "UsuarioSistema");

            migrationBuilder.DropIndex(
                name: "UQ_UsuarioSistema_Email",
                table: "UsuarioSistema");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "UsuarioSistema");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UsuarioSistema");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "UsuarioSistema");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "TramitacaoDocumento");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "TramitacaoDocumento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "TramitacaoDocumento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "TipoDocumentoColaborador");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "TipoDocumentoColaborador");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "TipoDocumentoColaborador");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "TipoDocumento");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "TipoDocumento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "TipoDocumento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Provincia");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Provincia");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Provincia");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PostoTrabalho");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "PostoTrabalho");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PostoTrabalho");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Perfil");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Perfil");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Perfil");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Pais");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Municipio");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Municipio");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Municipio");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "EstadoLogin");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "EstadoLogin");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "EstadoLogin");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "EstadoDocumento");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "EstadoDocumento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "EstadoDocumento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "EstadoColaborador");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "EstadoColaborador");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "EstadoColaborador");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "DocumentoHistorico");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "DocumentoHistorico");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "DocumentoHistorico");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "DocumentoAnexo");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "DocumentoAnexo");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "DocumentoAnexo");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Direcao");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Direcao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Direcao");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Colaborador");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Colaborador");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Colaborador");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "ClassificacaoDocumento");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "ClassificacaoDocumento");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "ClassificacaoDocumento");
        }
    }
}
