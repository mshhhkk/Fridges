using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fridges.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fridges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<short>(type: "smallint", nullable: false),
                    IsFreezer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    Instructions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    unitType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeProducts_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeProducts_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeatProducts",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFrozen = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeatProducts", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "MilkProducts",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FatPercent = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilkProducts", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "PlantProducts",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsOrganic = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantProducts", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FridgeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Release = table.Column<DateOnly>(type: "date", nullable: false),
                    Expiration = table.Column<DateOnly>(type: "date", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    IsFresh = table.Column<bool>(type: "boolean", nullable: false),
                    meatProductsProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    milkProductsProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    plantProductsProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalTable: "Fridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_MeatProducts_meatProductsProductId",
                        column: x => x.meatProductsProductId,
                        principalTable: "MeatProducts",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Products_MilkProducts_milkProductsProductId",
                        column: x => x.milkProductsProductId,
                        principalTable: "MilkProducts",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Products_PlantProducts_plantProductsProductId",
                        column: x => x.plantProductsProductId,
                        principalTable: "PlantProducts",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "Id", "Capacity", "IsFreezer", "Name" },
                values: new object[,]
                {
                    { new Guid("1aa76163-5ad2-4b36-a9cf-b04dd154a73f"), (short)7, true, "Samsung" },
                    { new Guid("e5820c2b-fc43-4a62-a717-b0eecb90e0b5"), (short)5, false, "Атлант" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name", "category" },
                values: new object[,]
                {
                    { 1, "Говядина", "0" },
                    { 2, "Свинина", "0" },
                    { 3, "Курица", "0" },
                    { 4, "Молоко", "0" },
                    { 5, "Сыр", "0" },
                    { 6, "Йогурт", "0" },
                    { 7, "Брокколи", "0" },
                    { 8, "Картофель", "0" },
                    { 9, "Яблоко", "0" },
                    { 10, "Хлеб", "0" },
                    { 11, "Томаты", "0" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Instructions", "title" },
                values: new object[] { new Guid("22b1b28a-7712-4f3a-a4b2-0b5d79c6c230"), "Some instructions...", "Simple chicken pasta" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Expiration", "FridgeId", "IsFresh", "ProductTypeId", "Release", "Weight", "meatProductsProductId", "milkProductsProductId", "plantProductsProductId" },
                values: new object[,]
                {
                    { new Guid("0fa2a1f6-fc70-45e5-82e4-e25aa6f49b95"), new DateOnly(2025, 7, 20), new Guid("1aa76163-5ad2-4b36-a9cf-b04dd154a73f"), true, 3, new DateOnly(2025, 7, 1), 0.5f, null, null, null },
                    { new Guid("96a7c8a5-9517-4a39-a990-0145b17b7f4c"), new DateOnly(2025, 7, 18), new Guid("e5820c2b-fc43-4a62-a717-b0eecb90e0b5"), true, 4, new DateOnly(2025, 7, 2), 1.2f, null, null, null },
                    { new Guid("a7e58b11-d61a-4df3-9cb3-2d1272a99115"), new DateOnly(2025, 8, 1), new Guid("1aa76163-5ad2-4b36-a9cf-b04dd154a73f"), true, 11, new DateOnly(2025, 7, 5), 0.3f, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "RecipeProducts",
                columns: new[] { "Id", "ProductTypeId", "Quantity", "RecipeId", "unitType" },
                values: new object[,]
                {
                    { new Guid("80ac650f-2158-4d5d-a38f-16e1287b89d4"), 3, 0.4f, new Guid("22b1b28a-7712-4f3a-a4b2-0b5d79c6c230"), "kg" },
                    { new Guid("96e30ad8-94d9-4dc0-9f79-8a13adfe6de9"), 4, 500f, new Guid("22b1b28a-7712-4f3a-a4b2-0b5d79c6c230"), "ml" },
                    { new Guid("b5384463-0404-42dc-b949-f0b33916efaa"), 11, 200f, new Guid("22b1b28a-7712-4f3a-a4b2-0b5d79c6c230"), "g" }
                });

            migrationBuilder.InsertData(
                table: "MeatProducts",
                columns: new[] { "ProductId", "IsFrozen", "Type" },
                values: new object[] { new Guid("0fa2a1f6-fc70-45e5-82e4-e25aa6f49b95"), true, 2 });

            migrationBuilder.InsertData(
                table: "MilkProducts",
                columns: new[] { "ProductId", "FatPercent", "Type" },
                values: new object[] { new Guid("96a7c8a5-9517-4a39-a990-0145b17b7f4c"), 3.5f, "Milk" });

            migrationBuilder.InsertData(
                table: "PlantProducts",
                columns: new[] { "ProductId", "IsOrganic", "Type" },
                values: new object[] { new Guid("a7e58b11-d61a-4df3-9cb3-2d1272a99115"), true, "Tomato" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_FridgeId",
                table: "Products",
                column: "FridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_meatProductsProductId",
                table: "Products",
                column: "meatProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_milkProductsProductId",
                table: "Products",
                column: "milkProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_plantProductsProductId",
                table: "Products",
                column: "plantProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProducts_ProductTypeId",
                table: "RecipeProducts",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProducts_RecipeId",
                table: "RecipeProducts",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeatProducts_Products_ProductId",
                table: "MeatProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MilkProducts_Products_ProductId",
                table: "MilkProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantProducts_Products_ProductId",
                table: "PlantProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeatProducts_Products_ProductId",
                table: "MeatProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MilkProducts_Products_ProductId",
                table: "MilkProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantProducts_Products_ProductId",
                table: "PlantProducts");

            migrationBuilder.DropTable(
                name: "RecipeProducts");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Fridges");

            migrationBuilder.DropTable(
                name: "MeatProducts");

            migrationBuilder.DropTable(
                name: "MilkProducts");

            migrationBuilder.DropTable(
                name: "PlantProducts");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
