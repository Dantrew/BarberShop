using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Migrations
{
    public partial class addedBirthyear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<float>(
                name: "BirthYear",
                table: "Customers",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Treatments",
                newName: "Prize");
        }
    }
}
