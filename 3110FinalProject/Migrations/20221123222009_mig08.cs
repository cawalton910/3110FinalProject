using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3110FinalProject.Migrations
{
    public partial class mig08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Purchases");
        }
    }
}
