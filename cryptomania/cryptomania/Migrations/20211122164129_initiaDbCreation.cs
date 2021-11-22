using Microsoft.EntityFrameworkCore.Migrations;

namespace cryptomania.Migrations
{
    public partial class initiaDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    MarketCap = table.Column<string>(nullable: true),
                    PriceChangePct = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptos");
        }
    }
}
