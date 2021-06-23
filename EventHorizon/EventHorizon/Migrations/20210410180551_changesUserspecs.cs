using Microsoft.EntityFrameworkCore.Migrations;

namespace EventHorizon.Migrations
{
    public partial class changesUserspecs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attendee");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Attendee",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Attendee",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attendee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
