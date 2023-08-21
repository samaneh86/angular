using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularEShop.DataLayer.Migrations
{
    public partial class updateproductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlTitle",
                table: "ProductCategories",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlTitle",
                table: "ProductCategories");
        }
    }
}
