using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workers.Migrations
{
    public partial class AddPersonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "personId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "personId",
                table: "AspNetUsers");
        }
    }
}
