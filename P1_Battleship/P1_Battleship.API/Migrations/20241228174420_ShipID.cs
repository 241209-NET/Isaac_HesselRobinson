using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_Battleship.Migrations
{
    /// <inheritdoc />
    public partial class ShipID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ships",
                table: "ships");

            migrationBuilder.AlterColumn<string>(
                name: "shipName",
                table: "ships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ships",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ships",
                table: "ships",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ships",
                table: "ships");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ships");

            migrationBuilder.AlterColumn<string>(
                name: "shipName",
                table: "ships",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ships",
                table: "ships",
                column: "shipName");
        }
    }
}
