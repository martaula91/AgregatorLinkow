using Microsoft.EntityFrameworkCore.Migrations;

namespace AgregatorLinkow.Data.Migrations
{
    public partial class ZliczaniePlusow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalityOfPluses",
                table: "Link",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalityOfPluses",
                table: "Link");
        }
    }
}
