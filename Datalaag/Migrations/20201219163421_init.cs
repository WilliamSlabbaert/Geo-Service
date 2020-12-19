using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalaag.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContinentData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RiverData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lenght = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiverData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CountryData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: false),
                    Surface = table.Column<double>(type: "float", nullable: false),
                    ContinentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CountryData_ContinentData_ContinentID",
                        column: x => x.ContinentID,
                        principalTable: "ContinentData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCapital = table.Column<bool>(type: "bit", nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CityData_CountryData_CountryID",
                        column: x => x.CountryID,
                        principalTable: "CountryData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryRiver",
                columns: table => new
                {
                    CountriesID = table.Column<int>(type: "int", nullable: false),
                    RiversID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRiver", x => new { x.CountriesID, x.RiversID });
                    table.ForeignKey(
                        name: "FK_CountryRiver_CountryData_CountriesID",
                        column: x => x.CountriesID,
                        principalTable: "CountryData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryRiver_RiverData_RiversID",
                        column: x => x.RiversID,
                        principalTable: "RiverData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityData_CountryID",
                table: "CityData",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_CountryData_ContinentID",
                table: "CountryData",
                column: "ContinentID");

            migrationBuilder.CreateIndex(
                name: "IX_CountryRiver_RiversID",
                table: "CountryRiver",
                column: "RiversID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityData");

            migrationBuilder.DropTable(
                name: "CountryRiver");

            migrationBuilder.DropTable(
                name: "CountryData");

            migrationBuilder.DropTable(
                name: "RiverData");

            migrationBuilder.DropTable(
                name: "ContinentData");
        }
    }
}
