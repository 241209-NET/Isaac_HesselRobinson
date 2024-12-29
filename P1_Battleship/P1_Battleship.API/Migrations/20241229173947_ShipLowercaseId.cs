using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_Battleship.Migrations
{
    /// <inheritdoc />
    public partial class ShipLowercaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ships",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ships",
                newName: "ID");
        }
    }
}
