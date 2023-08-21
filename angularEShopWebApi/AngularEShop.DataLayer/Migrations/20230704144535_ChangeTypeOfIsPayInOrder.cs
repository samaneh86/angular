using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularEShop.DataLayer.Migrations
{
    public partial class ChangeTypeOfIsPayInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPay",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IsPay",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
