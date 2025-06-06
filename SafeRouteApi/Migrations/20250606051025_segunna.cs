using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeRouteApi.Migrations
{
    /// <inheritdoc />
    public partial class segunna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreasComRiscos",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TipoDeRisco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasComRiscos", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "RotasSegurancas",
                columns: table => new
                {
                    RotaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Instrucoes = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PontosDePassagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AreaDeRiscoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AreasComRiscosAreaId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotasSegurancas", x => x.RotaId);
                    table.ForeignKey(
                        name: "FK_RotasSegurancas_AreasComRiscos_AreasComRiscosAreaId",
                        column: x => x.AreasComRiscosAreaId,
                        principalTable: "AreasComRiscos",
                        principalColumn: "AreaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RotasSegurancas_AreasComRiscosAreaId",
                table: "RotasSegurancas",
                column: "AreasComRiscosAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RotasSegurancas");

            migrationBuilder.DropTable(
                name: "AreasComRiscos");
        }
    }
}
