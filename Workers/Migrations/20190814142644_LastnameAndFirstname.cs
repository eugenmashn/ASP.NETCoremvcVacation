using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workers.Migrations
{
    public partial class LastnameAndFirstname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
               name: "LastName",
               table: "AspNetUsers",
               nullable: true);

            migrationBuilder.AddColumn<Guid>(
              name: "FirstName",
              table: "AspNetUsers",
              nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "LastName",
              table: "AspNetUsers");
                migrationBuilder.DropColumn(
              name: "FirstName",
              table: "AspNetUsers");

        }
    }
}
