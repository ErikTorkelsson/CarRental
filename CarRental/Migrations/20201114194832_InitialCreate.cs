using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(250)", nullable: true),
                    Model = table.Column<string>(type: "varchar(250)", nullable: true),
                    Year = table.Column<int>(type: "INT", nullable: false),
                    Mileage = table.Column<int>(type: "INT", nullable: false),
                    Fuel = table.Column<string>(type: "varchar(20)", nullable: true),
                    Seats = table.Column<int>(type: "INT", nullable: false),
                    combisudan = table.Column<string>(type: "varchar(20)", nullable: true),
                    about = table.Column<string>(type: "varchar(500)", nullable: true),
                    ImgSrc = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
