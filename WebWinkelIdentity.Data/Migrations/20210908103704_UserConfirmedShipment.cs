using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class UserConfirmedShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserThatConfirmedId",
                table: "Shipments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "52a5d716-a649-4476-b316-108d96c56112",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "906acae0-390a-4c5b-8a55-5a0ab0be502a", "AQAAAAEAACcQAAAAEMsXTRqaw/dx5+g93SiKavhpZcO6SGUGxcPtfP7SCb6O1TTmUBtDvN2CRJ8AeyL3gw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60992b03-8edf-4b11-9a0d-8ed82b88e306", "AQAAAAEAACcQAAAAEGWnt8DLA3T+Bcdr1R1PLMPFLHXZv1tPPdR+JDSGIGAD+R5F9C2LwBgUwyOkNNHVqQ==", "9f03fd67-9963-441c-9147-4f957bac2d62" });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_UserThatConfirmedId",
                table: "Shipments",
                column: "UserThatConfirmedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_UserThatConfirmedId",
                table: "Shipments",
                column: "UserThatConfirmedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_UserThatConfirmedId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_UserThatConfirmedId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UserThatConfirmedId",
                table: "Shipments");

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
    }
}
