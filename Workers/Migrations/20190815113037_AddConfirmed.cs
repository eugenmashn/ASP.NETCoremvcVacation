using Microsoft.EntityFrameworkCore.Migrations;

namespace Workers.Migrations
{
    public partial class AddConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedVacation",
                table: "Vacations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedVacation",
                table: "Vacations");
        }
    }
}
