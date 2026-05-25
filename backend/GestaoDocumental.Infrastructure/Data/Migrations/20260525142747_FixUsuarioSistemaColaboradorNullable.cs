using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDocumental.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioSistemaColaboradorNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ColaboradorId",
                table: "UsuarioSistema",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ColaboradorId",
                table: "UsuarioSistema",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
