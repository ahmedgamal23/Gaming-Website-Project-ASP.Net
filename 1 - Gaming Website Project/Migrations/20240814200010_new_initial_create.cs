using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1___Gaming_Website_Project.Migrations
{
    /// <inheritdoc />
    public partial class new_initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceGame");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeviceId",
                table: "Games",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Devices_DeviceId",
                table: "Games",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Devices_DeviceId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DeviceId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "DeviceGame",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGame", x => new { x.DevicesId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_DeviceGame_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceGame_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceGame_GamesId",
                table: "DeviceGame",
                column: "GamesId");
        }
    }
}
