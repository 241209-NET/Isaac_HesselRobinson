using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_Battleship.Migrations.Grid
{
    /// <inheritdoc />
    public partial class GridColumnsAsStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "columns",
                table: "grids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "columns",
                table: "grids");
        }
    }
}
