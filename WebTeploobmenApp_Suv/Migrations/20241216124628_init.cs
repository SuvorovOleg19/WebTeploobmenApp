using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTeploobmenApp_Suv.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    Diameter_of_pellets = table.Column<double>(type: "REAL", nullable: false),
                    Temp_pellets = table.Column<double>(type: "REAL", nullable: false),
                    Temp_air = table.Column<double>(type: "REAL", nullable: false),
                    Speed_air = table.Column<double>(type: "REAL", nullable: false),
                    Avg_heat_capacity = table.Column<double>(type: "REAL", nullable: false),
                    Consumption_of_pellets = table.Column<double>(type: "REAL", nullable: false),
                    Heat_capacity_of_pellets = table.Column<double>(type: "REAL", nullable: false),
                    Diameter = table.Column<double>(type: "REAL", nullable: false),
                    Volumetric_heat_transfer_coefficient = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variants");
        }
    }
}
