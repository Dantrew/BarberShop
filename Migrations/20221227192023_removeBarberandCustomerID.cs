using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Migrations
{
    public partial class removeBarberandCustomerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_Bookings_BookingId",
                table: "Barbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Bookings_BookingId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_BookingId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Barbers_BookingId",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Barbers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Barbers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BookingId",
                table: "Customers",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Barbers_BookingId",
                table: "Barbers",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_Bookings_BookingId",
                table: "Barbers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Bookings_BookingId",
                table: "Customers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
