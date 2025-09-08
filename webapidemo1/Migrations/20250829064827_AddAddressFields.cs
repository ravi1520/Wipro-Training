using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapidemo1.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "StudentAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "StudentAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PinCode",
                table: "StudentAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "StudentAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "StudentAddresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "StudentAddresses");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "StudentAddresses");

            migrationBuilder.DropColumn(
                name: "State",
                table: "StudentAddresses");
        }
    }
}
