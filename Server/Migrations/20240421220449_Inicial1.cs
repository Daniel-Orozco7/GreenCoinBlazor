using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenCoinHealth.Server.Migrations
{
    public partial class Inicial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    EsOrganico = table.Column<bool>(type: "bit", nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dietas",
                columns: table => new
                {
                    DietaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dietas", x => x.DietaId);
                });

            migrationBuilder.CreateTable(
                name: "Dificultades",
                columns: table => new
                {
                    DificultadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dificultades", x => x.DificultadId);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id_Gender = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name_Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id_Gender);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id_Role = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id_Role);
                });

            migrationBuilder.CreateTable(
                name: "Rutinas",
                columns: table => new
                {
                    RutinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutinas", x => x.RutinaId);
                });

            migrationBuilder.CreateTable(
                name: "DietaAlimentos",
                columns: table => new
                {
                    DietaId = table.Column<int>(type: "int", nullable: false),
                    AlimentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaAlimentos", x => new { x.DietaId, x.AlimentoId });
                    table.ForeignKey(
                        name: "FK_DietaAlimentos_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaAlimentos_Dietas_DietaId",
                        column: x => x.DietaId,
                        principalTable: "Dietas",
                        principalColumn: "DietaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Dni = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeDni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    username = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Id_Gender = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id_Role = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Dni);
                    table.ForeignKey(
                        name: "FK_User_Gender_Id_Gender",
                        column: x => x.Id_Gender,
                        principalTable: "Gender",
                        principalColumn: "Id_Gender",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Role",
                        principalColumn: "Id_Role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ejercicios",
                columns: table => new
                {
                    EjercicioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false),
                    Series = table.Column<int>(type: "int", nullable: false),
                    RutinaId = table.Column<int>(type: "int", nullable: false),
                    DificultadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicios", x => x.EjercicioId);
                    table.ForeignKey(
                        name: "FK_Ejercicios_Dificultades_DificultadId",
                        column: x => x.DificultadId,
                        principalTable: "Dificultades",
                        principalColumn: "DificultadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ejercicios_Rutinas_RutinaId",
                        column: x => x.RutinaId,
                        principalTable: "Rutinas",
                        principalColumn: "RutinaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietaAlimentos_AlimentoId",
                table: "DietaAlimentos",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicios_DificultadId",
                table: "Ejercicios",
                column: "DificultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicios_RutinaId",
                table: "Ejercicios",
                column: "RutinaId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id_Gender",
                table: "User",
                column: "Id_Gender");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id_Role",
                table: "User",
                column: "Id_Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaAlimentos");

            migrationBuilder.DropTable(
                name: "Ejercicios");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Alimentos");

            migrationBuilder.DropTable(
                name: "Dietas");

            migrationBuilder.DropTable(
                name: "Dificultades");

            migrationBuilder.DropTable(
                name: "Rutinas");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
