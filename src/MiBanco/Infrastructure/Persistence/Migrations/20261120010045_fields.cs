using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiBanco.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "transacciones",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "sucursales",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "cuentas",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "clientes",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "transacciones");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "sucursales");

            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "cuentas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "clientes");
        }
    }
}
