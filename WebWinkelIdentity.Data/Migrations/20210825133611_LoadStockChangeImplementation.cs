using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class LoadStockChangeImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStockChanges_AspNetUsers_UserId",
                table: "ProductStockChanges");

            migrationBuilder.DropIndex(
                name: "IX_ProductStockChanges_UserId",
                table: "ProductStockChanges");

            migrationBuilder.DropColumn(
                name: "DateChanged",
                table: "ProductStockChanges");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductStockChanges");

            migrationBuilder.AddColumn<int>(
                name: "LoadStockChangeId",
                table: "ProductStockChanges",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoadStockChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociatedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateChanged = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadStockChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoadStockChanges_AspNetUsers_AssociatedUserId",
                        column: x => x.AssociatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "473a4246-b1a3-47ec-96a4-a0c24ca50fef", "AQAAAAEAACcQAAAAEF4BJhJZzchBcj8AqLpdTZX10JmVtbtCpF0QpLeUyHJuXyC+OcZWyrCrq12NqZmXvg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bed99cce-a474-4d3b-bed7-c5f86490738e", "AQAAAAEAACcQAAAAELzVNoS7NRYX74/0CydsrAFbeK0YScUPp+f/O1GAB7IXbFV1/U4Av33ulVltRPQpUQ==", "faf0ac63-854b-4b59-88e4-ee9a34e861aa" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockChanges_LoadStockChangeId",
                table: "ProductStockChanges",
                column: "LoadStockChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoadStockChanges_AssociatedUserId",
                table: "LoadStockChanges",
                column: "AssociatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStockChanges_LoadStockChanges_LoadStockChangeId",
                table: "ProductStockChanges",
                column: "LoadStockChangeId",
                principalTable: "LoadStockChanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStockChanges_LoadStockChanges_LoadStockChangeId",
                table: "ProductStockChanges");

            migrationBuilder.DropTable(
                name: "LoadStockChanges");

            migrationBuilder.DropIndex(
                name: "IX_ProductStockChanges_LoadStockChangeId",
                table: "ProductStockChanges");

            migrationBuilder.DropColumn(
                name: "LoadStockChangeId",
                table: "ProductStockChanges");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateChanged",
                table: "ProductStockChanges",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductStockChanges",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3bbce7d1-8f6f-4f21-b3d3-3489502789a0", "AQAAAAEAACcQAAAAEKF3Fd7ktQLWLl1ooPJHjOK4b0wfO3pGgzt36dS9X5Arvx/nQS5g5BHAn0iqck/B/Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d56ed54-f021-4860-a4d8-e767fab793b4", "AQAAAAEAACcQAAAAENbvVDi7OErNrhMzPB9sij64qcWwMY27Pt2csW0CYjrpm3WsvNyuQ9fZK+gP/mHlYg==", "934e252e-ddc0-4da2-8c0e-15aceb9b3923" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStockChanges_UserId",
                table: "ProductStockChanges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStockChanges_AspNetUsers_UserId",
                table: "ProductStockChanges",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
