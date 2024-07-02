using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomPosApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MenuConfigurations",
                columns: table => new
                {
                    MenuConfigurationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuConfigurations", x => x.MenuConfigurationId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemId);
                });

            migrationBuilder.CreateTable(
                name: "MenuConfigurationCategories",
                columns: table => new
                {
                    MenuConfigurationCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuConfigurationId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
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
                name: "MenuItemOptions",
                columns: table => new
                {
                    MenuItemOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemOptions", x => x.MenuItemOptionId);
                    table.ForeignKey(
                        name: "FK_MenuItemOptions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOptions_MenuItemId",
                table: "MenuItemOptions",
                column: "MenuItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMenuItems");

            migrationBuilder.DropTable(
                name: "MenuConfigurationCategories");

            migrationBuilder.DropTable(
                name: "MenuItemOptions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "MenuConfigurations");

            migrationBuilder.DropTable(
                name: "MenuItems");
        }
    }
}
