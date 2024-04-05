using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCoinsValueToAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinsValue",
                table: "AvatarsUploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AvatarsUploads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoinsValue",
                value: 10);

            migrationBuilder.UpdateData(
                table: "AvatarsUploads",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoinsValue",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 1, 11, 9, 37, 542, DateTimeKind.Local).AddTicks(1998));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 1, 11, 9, 37, 542, DateTimeKind.Local).AddTicks(1959));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 1, 11, 9, 37, 542, DateTimeKind.Local).AddTicks(1887));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinsValue",
                table: "AvatarsUploads");

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 1, 10, 51, 46, 934, DateTimeKind.Local).AddTicks(7843));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 1, 10, 51, 46, 934, DateTimeKind.Local).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 1, 10, 51, 46, 934, DateTimeKind.Local).AddTicks(7712));
        }
    }
}
