using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SensorsControl.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyTelemetryEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTelemetryEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelemetryEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyEntityId = table.Column<int>(type: "int", nullable: false),
                    Illum = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemetryEntities_DailyTelemetryEntities_DailyEntityId",
                        column: x => x.DailyEntityId,
                        principalTable: "DailyTelemetryEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEntities_DailyEntityId",
                table: "TelemetryEntities",
                column: "DailyEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelemetryEntities");

            migrationBuilder.DropTable(
                name: "DailyTelemetryEntities");
        }
    }
}
