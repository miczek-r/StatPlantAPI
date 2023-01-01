using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RemovedIntervalFromTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "Trigger");

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenCalled",
                table: "Trigger",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Trigger",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenCalled",
                table: "Trigger");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Trigger");

            migrationBuilder.AddColumn<int>(
                name: "Interval",
                table: "Trigger",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
