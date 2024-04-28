using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenCoinHealth.Server.Migrations
{
    /// <inheritdoc />
    public partial class def : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ejercicios",
                table: "Rutinas",
                newName: "Ejercicioslist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ejercicioslist",
                table: "Rutinas",
                newName: "Ejercicios");
        }
    }
}
