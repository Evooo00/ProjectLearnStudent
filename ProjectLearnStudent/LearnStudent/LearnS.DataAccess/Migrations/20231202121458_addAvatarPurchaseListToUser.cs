using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addAvatarPurchaseListToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AvatarPurchases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 14, 58, 446, DateTimeKind.Local).AddTicks(3208));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 14, 58, 446, DateTimeKind.Local).AddTicks(3120));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 14, 58, 446, DateTimeKind.Local).AddTicks(3027));

            migrationBuilder.CreateIndex(
                name: "IX_AvatarPurchases_ApplicationUserId",
                table: "AvatarPurchases",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvatarPurchases_AspNetUsers_ApplicationUserId",
                table: "AvatarPurchases",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvatarPurchases_AspNetUsers_ApplicationUserId",
                table: "AvatarPurchases");

            migrationBuilder.DropIndex(
                name: "IX_AvatarPurchases_ApplicationUserId",
                table: "AvatarPurchases");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AvatarPurchases");

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 8, 32, 722, DateTimeKind.Local).AddTicks(5645));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 8, 32, 722, DateTimeKind.Local).AddTicks(5423));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 8, 32, 722, DateTimeKind.Local).AddTicks(5214));
        }
    }
}
