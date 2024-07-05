using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomPosApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuItemOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemId1",
                table: "MenuItemOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptions_MenuItemId1",
                table: "MenuItemOptions",
                column: "MenuItemId1",
                unique: true,
                filter: "[MenuItemId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOptions_MenuItems_MenuItemId1",
                table: "MenuItemOptions",
                column: "MenuItemId1",
                principalTable: "MenuItems",
                principalColumn: "MenuItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOptions_MenuItems_MenuItemId1",
                table: "MenuItemOptions");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemOptions_MenuItemId1",
                table: "MenuItemOptions");

            migrationBuilder.DropColumn(
                name: "MenuItemId1",
                table: "MenuItemOptions");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions",
                column: "MenuItemId",
                unique: true);
        }
    }
}
