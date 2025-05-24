using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInicialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Genre = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    IsActive = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    IsActive = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(254)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    UserType = table.Column<int>(type: "INT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    IsActive = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameSale",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "INT", nullable: false),
                    SalesId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSale", x => new { x.GamesId, x.SalesId });
                    table.ForeignKey(
                        name: "FK_GameSale_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSale_Sale_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Library_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartGame",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "INT", nullable: false),
                    GamesId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartGame", x => new { x.CartsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_CartGame_Cart_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartGame_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryGame",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "INT", nullable: false),
                    LibrariesId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryGame", x => new { x.GamesId, x.LibrariesId });
                    table.ForeignKey(
                        name: "FK_LibraryGame_Game_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryGame_Library_LibrariesId",
                        column: x => x.LibrariesId,
                        principalTable: "Library",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "CreationDate", "Description", "Genre", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 24, 18, 44, 15, 422, DateTimeKind.Local).AddTicks(4905), "A vast action RPG world", "RPG", true, "Elden Ring", 299.99m },
                    { 2, new DateTime(2025, 5, 24, 18, 44, 15, 422, DateTimeKind.Local).AddTicks(4907), "Farming and life simulator", "Simulation", true, "Stardew Valley", 39.99m },
                    { 3, new DateTime(2025, 5, 24, 18, 44, 15, 422, DateTimeKind.Local).AddTicks(4909), "Roguelike action-packed dungeon crawler", "Action", true, "Hades", 79.99m },
                    { 4, new DateTime(2025, 5, 24, 18, 44, 15, 422, DateTimeKind.Local).AddTicks(4910), "Challenging platformer with a touching story", "Platformer", true, "Celeste", 49.99m },
                    { 5, new DateTime(2025, 5, 24, 18, 44, 15, 422, DateTimeKind.Local).AddTicks(4912), "Open-world fantasy RPG with deep narrative", "RPG", true, "The Witcher 3", 119.99m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreationDate", "Email", "IsActive", "Password", "UserName", "UserType" },
                values: new object[] { -1, new DateTime(2025, 5, 24, 18, 44, 15, 422, DateTimeKind.Local).AddTicks(4774), "admin@admin.com.br", true, "AQAAAAEAACcQAAAAECeoJ4RUQe9tBkQjHXoUorXRaMWvJoHLp4gG/h5vvxuRNQtLULdIp5NC0tFn5/e14w==", "admin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartGame_GamesId",
                table: "CartGame",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSale_SalesId",
                table: "GameSale",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Library_UserId",
                table: "Library",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryGame_LibrariesId",
                table: "LibraryGame",
                column: "LibrariesId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartGame");

            migrationBuilder.DropTable(
                name: "GameSale");

            migrationBuilder.DropTable(
                name: "LibraryGame");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Library");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
