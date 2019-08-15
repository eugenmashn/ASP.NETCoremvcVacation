using Microsoft.EntityFrameworkCore.Migrations;

namespace Workers.Migrations
{
    public partial class AddPersoninUSer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_personId",
                table: "AspNetUsers",
                column: "personId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_People_personId",
                table: "AspNetUsers",
                column: "personId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_People_personId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_personId",
                table: "AspNetUsers");
        }
    }
}
