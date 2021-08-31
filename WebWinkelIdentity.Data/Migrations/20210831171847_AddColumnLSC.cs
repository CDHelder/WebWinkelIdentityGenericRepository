using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class AddColumnLSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "LoadStockChanges",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a6d42ddd-63dd-47e4-a568-0755b0d404f0", "AQAAAAEAACcQAAAAENwLbI2cn/8QLXxBs/w64Yp0snA8bRcCaSbY788Rn413js5NfKWvvqKxbaDQEB6sDg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9684236-13ae-4b11-8208-8395fb5187ce", "AQAAAAEAACcQAAAAEFfERc9ZeNZ7Ba0XlyE0aauUENurQhv263bHRXIKrsA5+WL46aqZthwJp4HwnWooSg==", "23e2e6f0-3f34-43a7-84c9-70dbfc74b1d8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "LoadStockChanges");

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
        }
    }
}
