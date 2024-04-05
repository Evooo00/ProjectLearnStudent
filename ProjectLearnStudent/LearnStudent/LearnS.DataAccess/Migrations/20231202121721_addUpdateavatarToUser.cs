using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUpdateavatarToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 17, 21, 30, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 17, 21, 30, DateTimeKind.Local).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 2, 13, 17, 21, 30, DateTimeKind.Local).AddTicks(8364));

            migrationBuilder.CreateIndex(
                name: "IX_AvatarPurchases_AvatarId",
                table: "AvatarPurchases",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvatarPurchases_AvatarsUploads_AvatarId",
                table: "AvatarPurchases",
                column: "AvatarId",
                principalTable: "AvatarsUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvatarPurchases_AvatarsUploads_AvatarId",
                table: "AvatarPurchases");

            migrationBuilder.DropIndex(
                name: "IX_AvatarPurchases_AvatarId",
                table: "AvatarPurchases");

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
        }
    }
}
