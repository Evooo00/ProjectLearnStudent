using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForumModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_ForumPosts_ForumPostId",
                table: "ForumComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_ForumThreads_ForumThreadId",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRatings_ForumPosts_ForumPostId",
                table: "ForumRatings");

            migrationBuilder.AddColumn<int>(
                name: "ReplyCount",
                table: "ForumThreads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 19, 37, 43, 567, DateTimeKind.Local).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 19, 37, 43, 567, DateTimeKind.Local).AddTicks(9482));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReplyCount" },
                values: new object[] { new DateTime(2023, 11, 26, 19, 37, 43, 567, DateTimeKind.Local).AddTicks(9415), 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_ForumPosts_ForumPostId",
                table: "ForumComments",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
    name: "FK_ForumPosts_ForumThreads_ForumThreadId",
    table: "ForumPosts",
    column: "ForumThreadId",
    principalTable: "ForumThreads",
    principalColumn: "Id",
    onDelete: ReferentialAction.NoAction); ;

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRatings_ForumPosts_ForumPostId",
                table: "ForumRatings",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumComments_ForumPosts_ForumPostId",
                table: "ForumComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_ForumThreads_ForumThreadId",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumRatings_ForumPosts_ForumPostId",
                table: "ForumRatings");

            migrationBuilder.DropColumn(
                name: "ReplyCount",
                table: "ForumThreads");

            migrationBuilder.UpdateData(
                table: "ForumComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 23, 12, 22, 23, 67, DateTimeKind.Local).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "ForumPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 23, 12, 22, 23, 67, DateTimeKind.Local).AddTicks(3591));

            migrationBuilder.UpdateData(
                table: "ForumThreads",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 23, 12, 22, 23, 67, DateTimeKind.Local).AddTicks(3518));

            migrationBuilder.AddForeignKey(
                name: "FK_ForumComments_ForumPosts_ForumPostId",
                table: "ForumComments",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_ForumThreads_ForumThreadId",
                table: "ForumPosts",
                column: "ForumThreadId",
                principalTable: "ForumThreads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumRatings_ForumPosts_ForumPostId",
                table: "ForumRatings",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "Id");
        }
    }
}
