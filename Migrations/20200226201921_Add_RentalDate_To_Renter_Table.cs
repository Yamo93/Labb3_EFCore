using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompactDiscProject.Migrations
{
    public partial class Add_RentalDate_To_Renter_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RentalDate",
                table: "Renter",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalDate",
                table: "Renter");
        }
    }
}
