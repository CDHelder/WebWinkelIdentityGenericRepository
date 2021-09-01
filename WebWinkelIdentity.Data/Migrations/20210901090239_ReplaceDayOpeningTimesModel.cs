using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebWinkelIdentity.Data.Migrations
{
    public partial class ReplaceDayOpeningTimesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOpeningTimes");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayClosingTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayOpeningTime",
                table: "WeekOpeningTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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

            migrationBuilder.UpdateData(
                table: "WeekOpeningTimes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FridayClosingTime", "FridayOpeningTime", "MondayClosingTime", "MondayOpeningTime", "SaturdayClosingTime", "SaturdayOpeningTime", "ThursdayClosingTime", "ThursdayOpeningTime", "TuesdayClosingTime", "TuesdayOpeningTime", "WednesdayClosingTime", "WednesdayOpeningTime" },
                values: new object[] { new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FridayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "FridayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "MondayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "MondayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "SaturdayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "SaturdayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "SundayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "SundayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "ThursdayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "ThursdayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "TuesdayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "TuesdayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "WednesdayClosingTime",
                table: "WeekOpeningTimes");

            migrationBuilder.DropColumn(
                name: "WednesdayOpeningTime",
                table: "WeekOpeningTimes");

            migrationBuilder.CreateTable(
                name: "DayOpeningTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    WeekOpeningTimesId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DayOpeningTimes_WeekOpeningTimesId",
                table: "DayOpeningTimes",
                column: "WeekOpeningTimesId");
        }
    }
}
