using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class CreateShipmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivered = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    LoadStockChangeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_LoadStockChanges_LoadStockChangeId",
                        column: x => x.LoadStockChangeId,
                        principalTable: "LoadStockChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "da5c0e6d-3719-45d1-8978-f38bac617afc", "AQAAAAEAACcQAAAAEEUNJNju3PaDGosne2c1lhZgIiNpBwKs30KMaFj2GXqO+1ihTF378sa+FlWXmRw+eQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e14b14c1-d2e7-4833-964d-fcb87c597093", "AQAAAAEAACcQAAAAEFzEB7GV5Vc53Okfw/8DBAeMfFe8U+TgxIYaswxPpyklRrmugE4/TU9gaF1+0FtboA==", "f992b81d-3817-47cd-ab70-7356cfc3caf9" });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_LoadStockChangeId",
                table: "Shipments",
                column: "LoadStockChangeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_StoreId",
                table: "Shipments",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "753cbb99-e109-4e7b-b4e7-0b289adf14bc", "AQAAAAEAACcQAAAAEIRdZ0G9iOVOlOsJG4+9q4C1Y8E23Z8sFS15uOEQ6gSppkzSwC013hpmDmGX2ZCKxw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4d5276b-4874-4b25-be2a-07839be774a1", "AQAAAAEAACcQAAAAEDd2t3fzUWOC3uI2Q0ak4sDb6uX2IQQ7Q3zpJCuyDaSDbokCYj6SzfB4FLKwhoLXMQ==", "d582a175-408d-4c36-88e5-3b4d4a8440c0" });
        }
    }
}
