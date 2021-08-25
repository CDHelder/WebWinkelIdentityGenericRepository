using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class AddForeignKeyReferenceLoadStockChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadStockChanges_AspNetUsers_AssociatedUserId",
                table: "LoadStockChanges");

            migrationBuilder.DropIndex(
                name: "IX_LoadStockChanges_AssociatedUserId",
                table: "LoadStockChanges");

            migrationBuilder.DropColumn(
                name: "AssociatedUserId",
                table: "LoadStockChanges");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LoadStockChanges",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af0c1c84-9d4e-4405-ab85-6b18cebd560d", "AQAAAAEAACcQAAAAEDXtG2Q4csFQ/4SJ9eu2uXYJwmb8ZwCbu/Lvm/V8X7tcY8bGXvX8s9ltRR2j5waCZQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c7e9619-b422-4846-b6d4-12978ac4e656", "AQAAAAEAACcQAAAAEC898DbKlom+Px88MNTPlUQk+BVu72KPCfh5CmR91bTInhwANYMtjrqizzLDXk97Sg==", "ea22acef-b2d3-4779-84ce-4892df9c24fb" });

            migrationBuilder.CreateIndex(
                name: "IX_LoadStockChanges_UserId",
                table: "LoadStockChanges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadStockChanges_AspNetUsers_UserId",
                table: "LoadStockChanges",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadStockChanges_AspNetUsers_UserId",
                table: "LoadStockChanges");

            migrationBuilder.DropIndex(
                name: "IX_LoadStockChanges_UserId",
                table: "LoadStockChanges");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LoadStockChanges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssociatedUserId",
                table: "LoadStockChanges",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "IX_LoadStockChanges_AssociatedUserId",
                table: "LoadStockChanges",
                column: "AssociatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadStockChanges_AspNetUsers_AssociatedUserId",
                table: "LoadStockChanges",
                column: "AssociatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
