using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class NavigationPropLSChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "LoadStockChanges",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d18b13a7-84f8-4df7-94d9-559b81c528b7", "AQAAAAEAACcQAAAAEAWtvaw2g2PTnBGC4ZvvAaGuZt+S3mSKKAkbyFZrX49NBiJ0+RGOZ0oT7a2xFI9Vxg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac73fccf-5076-487c-9819-9263b7b8b4bf", "AQAAAAEAACcQAAAAEI9PyNxiHRvvveDoi9X4mOR8gSyaEsurEugZ8JQ2mjVimzpNmBgJUy2GnLmv0IqL1g==", "414299d1-d07f-41d6-9dcf-63593fd1a4d6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "LoadStockChanges");

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
        }
    }
}
