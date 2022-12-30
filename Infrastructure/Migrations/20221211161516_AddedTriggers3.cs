using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedTriggers3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trigger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TriggerType = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trigger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trigger_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Inequality = table.Column<int>(type: "int", nullable: false),
                    SensorTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<float>(type: "float", nullable: false),
                    TriggerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condition_SensorType_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "SensorType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Condition_Trigger_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "Trigger",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Condition_SensorTypeId",
                table: "Condition",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Condition_TriggerId",
                table: "Condition",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trigger_DeviceId",
                table: "Trigger",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "Trigger");
        }
    }
}
