using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddCoffeeShopsInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"  INSERT INTO CoffeeShops (Name,OpeningHours,Address) " +
                $"VALUES('Emrul Coffee House', '8-9 Sun-Fri', 'Kallanpur, Dhaka')");
            migrationBuilder.Sql($"  INSERT INTO CoffeeShops (Name,OpeningHours,Address) " +
                $"VALUES('Raju Coffee House', '8-9 Sun-Fri', 'Symoli, Dhaka')");
            migrationBuilder.Sql($"  INSERT INTO CoffeeShops (Name,OpeningHours,Address) " +
                $"VALUES('Rabbi Coffee House', '8-9 Sun-Fri', 'Hatirjhil, Dhaka')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
