using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeladeriaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Apifinalizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salsas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsSinTac = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salsas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Helados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    IsArtesanal = table.Column<bool>(type: "bit", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Helados_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredienteSalsa",
                columns: table => new
                {
                    IngredienteId = table.Column<int>(type: "int", nullable: false),
                    SalsaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteSalsa", x => new { x.IngredienteId, x.SalsaId });
                    table.ForeignKey(
                        name: "FK_IngredienteSalsa_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredienteSalsa_Salsas_SalsaId",
                        column: x => x.SalsaId,
                        principalTable: "Salsas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredienteHelado",
                columns: table => new
                {
                    IngredienteId = table.Column<int>(type: "int", nullable: false),
                    HeladoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteHelado", x => new { x.HeladoId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_IngredienteHelado_Helados_HeladoId",
                        column: x => x.HeladoId,
                        principalTable: "Helados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredienteHelado_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Disponible" },
                    { 2, "Pendiente" },
                    { 3, "No Disponible" }
                });

            migrationBuilder.InsertData(
                table: "Ingredientes",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Leche" },
                    { 2, "Azucar" },
                    { 3, "Alcohol" },
                    { 4, "Chocolate" },
                    { 5, "Crema" },
                    { 6, "Agua" },
                    { 7, "Frutilla" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estados_Nombre",
                table: "Estados",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Helados_EstadoId",
                table: "Helados",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteHelado_IngredienteId",
                table: "IngredienteHelado",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_Nombre",
                table: "Ingredientes",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredienteSalsa_SalsaId",
                table: "IngredienteSalsa",
                column: "SalsaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredienteHelado");

            migrationBuilder.DropTable(
                name: "IngredienteSalsa");

            migrationBuilder.DropTable(
                name: "Helados");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Salsas");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
