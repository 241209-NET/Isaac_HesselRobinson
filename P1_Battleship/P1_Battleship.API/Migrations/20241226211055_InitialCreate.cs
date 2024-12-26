using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_Battleship.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ships",
                columns: table => new
                {
                    shipName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    positions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hitPoints = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ships", x => x.shipName);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ships");
        }
    }
}
