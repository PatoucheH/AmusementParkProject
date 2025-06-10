using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmusementPark.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurePositionAsOwnedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Budget = table.Column<double>(type: "REAL", nullable: false),
                    VisitorsEntry = table.Column<int>(type: "INTEGER", nullable: false),
                    VisitorsOut = table.Column<int>(type: "INTEGER", nullable: false),
                    VisitorInPark = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalVisitors = table.Column<int>(type: "INTEGER", nullable: false),
                    GridParkJson = table.Column<string>(type: "TEXT", nullable: true),
                    InventoryBuildingsJson = table.Column<string>(type: "TEXT", nullable: false),
                    PlacedBuildingJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parks");
        }
    }
}
