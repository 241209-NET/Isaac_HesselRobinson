using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_Battleship.Migrations.Grid
{
    /// <inheritdoc />
    public partial class GridShipArray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "shipIds",
                table: "grids",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shipIds",
                table: "grids");
        }
    }
}
