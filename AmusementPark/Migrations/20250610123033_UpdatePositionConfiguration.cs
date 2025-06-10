using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmusementPark.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePositionConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DuckFishing",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    MaintenancePrice = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VisitorInAttraction = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_X = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuckFishing", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FoodShop",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: true),
                    MaintenancePrice = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VisitorInAttraction = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_X = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodShop", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "GiftShop",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: true),
                    MaintenancePrice = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VisitorInAttraction = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_X = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftShop", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "HauntedHouse",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    MaintenancePrice = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VisitorInAttraction = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_X = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HauntedHouse", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "RollerCoaster",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Emoji = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    MaintenancePrice = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VisitorInAttraction = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_X = table.Column<int>(type: "INTEGER", nullable: false),
                    Ordinal_Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollerCoaster", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DuckFishing");

            migrationBuilder.DropTable(
                name: "FoodShop");

            migrationBuilder.DropTable(
                name: "GiftShop");

            migrationBuilder.DropTable(
                name: "HauntedHouse");

            migrationBuilder.DropTable(
                name: "RollerCoaster");
        }
    }
}
