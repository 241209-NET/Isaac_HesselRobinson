using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_Battleship.Migrations
{
    /// <inheritdoc />
    public partial class ShipType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "ships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "ships");
        }
    }
}
