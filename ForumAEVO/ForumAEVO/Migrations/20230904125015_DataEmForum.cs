using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumAEVO.Migrations
{
    /// <inheritdoc />
    public partial class DataEmForum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Topico_TopicoId",
                table: "Comentario");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Topico",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicoId",
                table: "Comentario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Comentario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Topico_TopicoId",
                table: "Comentario",
                column: "TopicoId",
                principalTable: "Topico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // Alterado para Restrict
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Topico_TopicoId",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Topico");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Comentario");

            migrationBuilder.AlterColumn<Guid>(
                name: "TopicoId",
                table: "Comentario",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Topico_TopicoId",
                table: "Comentario",
                column: "TopicoId",
                principalTable: "Topico",
                principalColumn: "Id");
        }
    }
}
