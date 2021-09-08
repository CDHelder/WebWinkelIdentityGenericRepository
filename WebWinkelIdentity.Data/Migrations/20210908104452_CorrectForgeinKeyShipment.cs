using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class CorrectForgeinKeyShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_UserThatConfirmedId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_UserThatConfirmedId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UserThatConfirmedId",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shipments",
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
                values: new object[] { "a2326cf1-fe52-4379-8103-dcf37f5beaef", "AQAAAAEAACcQAAAAEFiYpDBunNJIm6MWnCOr0RmS7NT/cpwHuVGJKXZGtE48OaP3kRiDPGy8Gr4F3P0OOw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7036d951-7cc8-488f-b95b-10c2e96c31c9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c4ecef0-bc2d-4dfa-a036-c210831af10e", "AQAAAAEAACcQAAAAEDxc7NzivKVyomNdAr8xbV1IiUSicSZGTAmboLz+4o/tLyjJbZLW+nh0t3h/36CgnQ==", "61081e62-bffc-4fe8-87cd-24f97436dbb3" });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_UserId",
                table: "Shipments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_UserId",
                table: "Shipments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_UserId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_UserId",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
