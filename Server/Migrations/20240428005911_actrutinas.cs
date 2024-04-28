using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenCoinHealth.Server.Migrations
{
    /// <inheritdoc />
    public partial class actrutinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dificultad",
                table: "Rutinas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dificultad",
                table: "Rutinas");
        }
    }
}
