using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAPI.Migrations
{
    public partial class ic2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Addresses",
                newName: "Pincode");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Addresses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Addresses",
                newName: "AddressLine2");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Addresses",
                newName: "AddressId");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Pincode",
                table: "Addresses",
                newName: "Nationality");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Addresses",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Addresses",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "AuthorId");
        }
    }
}
