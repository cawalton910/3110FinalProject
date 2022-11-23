using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3110FinalProject.Migrations
{
    public partial class mig03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductPurchase");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductPurchase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
