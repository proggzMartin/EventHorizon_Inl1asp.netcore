using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventHorizon.Migrations
{
    public partial class EventImageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "EventImage",
                table: "Event",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventImage",
                table: "Event");
        }
    }
}
