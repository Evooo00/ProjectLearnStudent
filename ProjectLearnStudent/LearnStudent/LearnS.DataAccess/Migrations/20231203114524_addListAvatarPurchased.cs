using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addListAvatarPurchased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvatarsUploadId",
                table: "AvatarPurchases",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 3, 12, 45, 24, 69, DateTimeKind.Local).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 3, 12, 45, 24, 69, DateTimeKind.Local).AddTicks(6905));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 3, 12, 45, 24, 69, DateTimeKind.Local).AddTicks(6819));

            migrationBuilder.CreateIndex(
                name: "IX_AvatarPurchases_AvatarsUploadId",
                table: "AvatarPurchases",
                column: "AvatarsUploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvatarPurchases_AvatarsUploads_AvatarsUploadId",
                table: "AvatarPurchases",
                column: "AvatarsUploadId",
                principalTable: "AvatarsUploads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvatarPurchases_AvatarsUploads_AvatarsUploadId",
                table: "AvatarPurchases");

            migrationBuilder.DropIndex(
                name: "IX_AvatarPurchases_AvatarsUploadId",
                table: "AvatarPurchases");

            migrationBuilder.DropColumn(
                name: "AvatarsUploadId",
                table: "AvatarPurchases");

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
        }
    }
}
