using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Forum.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class migration_carga_inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Topicos",
                columns: new[] { "Id", "Descricao", "Titulo", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Carros sao caros", "Carros", 1 },
                    { 2, "Comidas sao cozidas", "Comida", 2 },
                    { 3, "Canetas bic", "Materiais Escolares", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topicos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Topicos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Topicos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
