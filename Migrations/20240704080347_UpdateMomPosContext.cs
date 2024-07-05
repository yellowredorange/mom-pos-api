using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomPosApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMomPosContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMenuItems");

            migrationBuilder.DropTable(
                name: "MenuConfigurationCategories");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "MenuItemOptions");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "Categories",
                newName: "MenuConfigurationId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "AdditionalPrice",
                table: "MenuItemOptions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions",
                column: "MenuItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MenuConfigurationId",
                table: "Categories",
                column: "MenuConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_MenuConfigurations_MenuConfigurationId",
                table: "Categories",
                column: "MenuConfigurationId",
                principalTable: "MenuConfigurations",
                principalColumn: "MenuConfigurationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_MenuConfigurations_MenuConfigurationId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MenuConfigurationId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "AdditionalPrice",
                table: "MenuItemOptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "MenuConfigurationId",
                table: "Categories",
                newName: "SortOrder");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "MenuItemOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryMenuItems",
                columns: table => new
                {
                    CategoryMenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMenuItems", x => x.CategoryMenuItemId);
                    table.ForeignKey(
                        name: "FK_CategoryMenuItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMenuItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuConfigurationCategories",
                columns: table => new
                {
                    MenuConfigurationCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MenuConfigurationId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuConfigurationCategories", x => x.MenuConfigurationCategoryId);
                    table.ForeignKey(
                        name: "FK_MenuConfigurationCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuConfigurationCategories_MenuConfigurations_MenuConfigurationId",
                        column: x => x.MenuConfigurationId,
                        principalTable: "MenuConfigurations",
                        principalColumn: "MenuConfigurationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMenuItems_CategoryId",
                table: "CategoryMenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMenuItems_MenuItemId",
                table: "CategoryMenuItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuConfigurationCategories_CategoryId",
                table: "MenuConfigurationCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuConfigurationCategories_MenuConfigurationId",
                table: "MenuConfigurationCategories",
                column: "MenuConfigurationId");
        }
    }
}
