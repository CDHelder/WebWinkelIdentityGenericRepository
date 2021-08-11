﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class CreateDatabaseAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekOpeningTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekOpeningTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    Streetname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayOpeningTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekOpeningTimesId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOpeningTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayOpeningTimes_WeekOpeningTimes_WeekOpeningTimesId",
                        column: x => x.WeekOpeningTimesId,
                        principalTable: "WeekOpeningTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyEmployed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    WeekOpeningTimesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stores_WeekOpeningTimes_WeekOpeningTimesId",
                        column: x => x.WeekOpeningTimesId,
                        principalTable: "WeekOpeningTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fabric = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotterdamStock = table.Column<int>(type: "int", nullable: false),
                    HaarlemStock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreEmployees_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CustomerId", "HouseNumber", "PostalCode", "Streetname", "SupplierId" },
                values: new object[,]
                {
                    { 3, "Den Haag", "Netherlands", null, 26, "8137 YA", "Korte poten", null },
                    { 4, "Rotterdam", "Netherlands", null, 12, "6573 IK", "Lijnbaan", null },
                    { 5, "Haarlem", "Netherlands", null, 18, "2756 IK", "Zijlstraat", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "52a5d716-a649-4476-b316-108d96c56112", 0, "961686ee-766f-457f-b971-72d983fb6eb6", "Jaap@gmail.com", true, false, null, null, null, "AQAAAAEAACcQAAAAEIksm09b31HZ1jorYlbAwhjJ9QWuRxN5Y6gXj8CEzNC6rlmvw9YADtLXi3VWYsxT+w==", null, false, "", false, "Jaap123" },
                    { "7036d951-7cc8-488f-b95b-10c2e96c31c9", 0, "e0b62dab-12a6-4805-a84e-3607b00037ba", "Samantha@gmail.com", true, false, null, null, null, "AQAAAAEAACcQAAAAEFJOlVPLrwwaV7L0D9/YLfL1JU/4BGBzX0Vy/xb3G4Akklr7vxxRwFeafEePbOZ32g==", null, false, "984b062f-8b4c-4886-a99f-5d7e4b51a2da", false, "Samantha123" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Broek met wijde pijpen", "Broek" },
                    { 2, "T-shirt met korte mouwen", "T-shirt" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Description", "Email", "Name", "PhoneNumber" },
                values: new object[] { 1, "groothandel merkkleding", "GroothandelDeBos@gmail.com", "Kleding Groothandel de Bos", 1012346543 });

            migrationBuilder.InsertData(
                table: "WeekOpeningTimes",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CustomerId", "HouseNumber", "PostalCode", "Streetname", "SupplierId" },
                values: new object[] { 1, "Amsterdam", "Netherlands", null, 15, "1264 KJ", "Polderweg", 1 });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "SupplierId" },
                values: new object[,]
                {
                    { 1, "Veel te duur", "Gucci", 1 },
                    { 2, "Veel te duur", "Versace", 1 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name" },
                values: new object[] { "52a5d716-a649-4476-b316-108d96c56112", "Jaap" });

            migrationBuilder.InsertData(
                table: "DayOpeningTimes",
                columns: new[] { "Id", "ClosingTime", "Day", "IsClosed", "OpeningTime", "WeekOpeningTimesId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 17, 0, 0, 0), "Monday", false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 2, new TimeSpan(0, 17, 0, 0, 0), "Tuesday", false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 3, new TimeSpan(0, 17, 0, 0, 0), "Wednesday", false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 4, new TimeSpan(0, 17, 0, 0, 0), "Thursday", false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 5, new TimeSpan(0, 17, 0, 0, 0), "Friday", false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 6, new TimeSpan(0, 17, 0, 0, 0), "Saterday", false, new TimeSpan(0, 9, 0, 0, 0), 1 },
                    { 7, null, "Sunday", true, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AddressId", "CurrentlyEmployed", "IBAN", "Name" },
                values: new object[] { "7036d951-7cc8-488f-b95b-10c2e96c31c9", 3, true, "NL76 INGB 007 4201 6969", "Samantha" });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "AddressId", "WeekOpeningTimesId" },
                values: new object[,]
                {
                    { 2, 5, null },
                    { 1, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CustomerId", "HouseNumber", "PostalCode", "Streetname", "SupplierId" },
                values: new object[] { 2, "Rotterdam", "Netherlands", "52a5d716-a649-4476-b316-108d96c56112", 5, "7431 GG", "Sesamstraat", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "Description", "Fabric", "HaarlemStock", "Name", "Price", "RotterdamStock", "Size" },
                values: new object[,]
                {
                    { 15, 2, 1, "Dark-Blue", "Donkere broek met versace logo", "100% Cotton", 2, "Versace Broek", 69.95m, 2, "L" },
                    { 14, 2, 1, "Dark-Blue", "Donkere broek met versace logo", "100% Cotton", 2, "Versace Broek", 69.95m, 2, "M" },
                    { 13, 2, 1, "Dark-Blue", "Donkere broek met versace logo", "100% Cotton", 1, "Versace Broek", 69.95m, 2, "S" },
                    { 12, 2, 2, "Light-Yellow", "Licht shirt met versace logo", "100% Cotton", 2, "Versace T-shirt", 45.95m, 1, "XL" },
                    { 11, 2, 2, "Light-Yellow", "Licht shirt met versace logo", "100% Cotton", 2, "Versace T-shirt", 45.95m, 2, "L" },
                    { 10, 2, 2, "Light-Yellow", "Licht shirt met versace logo", "100% Cotton", 2, "Versace T-shirt", 45.95m, 2, "M" },
                    { 9, 2, 2, "Light-Yellow", "Licht shirt met versace logo", "100% Cotton", 1, "Versace T-shirt", 45.95m, 2, "S" },
                    { 16, 2, 1, "Dark-Blue", "Donkere broek met versace logo", "100% Cotton", 2, "Versace Broek", 69.95m, 1, "XL" },
                    { 8, 1, 1, "Light-Blue", "Lichte broek met gucci logo", "100% Cotton", 2, "Gucci Broek", 59.95m, 1, "XL" },
                    { 6, 1, 1, "Light-Blue", "Lichte broek met gucci logo", "100% Cotton", 2, "Gucci Broek", 59.95m, 2, "M" },
                    { 5, 1, 1, "Light-Blue", "Lichte broek met gucci logo", "100% Cotton", 1, "Gucci Broek", 59.95m, 2, "S" },
                    { 4, 1, 2, "White", "Witte kleur met gucci logo", "100% Cotton", 2, "Gucci T-shirt", 39.95m, 1, "XL" },
                    { 3, 1, 2, "White", "Witte kleur met gucci logo", "100% Cotton", 2, "Gucci T-shirt", 39.95m, 2, "L" },
                    { 2, 1, 2, "White", "Witte kleur met gucci logo", "100% Cotton", 2, "Gucci T-shirt", 39.95m, 2, "M" },
                    { 1, 1, 2, "White", "Witte kleur met gucci logo", "100% Cotton", 1, "Gucci T-shirt", 39.95m, 2, "S" },
                    { 7, 1, 1, "Light-Blue", "Lichte broek met gucci logo", "100% Cotton", 2, "Gucci Broek", 59.95m, 2, "L" }
                });

            migrationBuilder.InsertData(
                table: "StoreEmployees",
                columns: new[] { "Id", "EmployeeId", "StoreId" },
                values: new object[,]
                {
                    { 2, "7036d951-7cc8-488f-b95b-10c2e96c31c9", 2 },
                    { 1, "7036d951-7cc8-488f-b95b-10c2e96c31c9", 1 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressId", "CustomerId", "IsDelivered" },
                values: new object[] { 1, 2, "52a5d716-a649-4476-b316-108d96c56112", false });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 1 },
                    { 3, 1, 3, 1 },
                    { 4, 1, 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SupplierId",
                table: "Addresses",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SupplierId",
                table: "Brands",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DayOpeningTimes_WeekOpeningTimesId",
                table: "DayOpeningTimes",
                column: "WeekOpeningTimesId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressId",
                table: "Employees",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreEmployees_EmployeeId",
                table: "StoreEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreEmployees_StoreId",
                table: "StoreEmployees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AddressId",
                table: "Stores",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_WeekOpeningTimesId",
                table: "Stores",
                column: "WeekOpeningTimesId",
                unique: true,
                filter: "[WeekOpeningTimesId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOpeningTimes");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "StoreEmployees");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "WeekOpeningTimes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112");
        }
    }
}
