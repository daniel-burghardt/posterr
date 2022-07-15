using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Posterr.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    JoinedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(777)", maxLength: 777, nullable: true),
                    QuotedPostId = table.Column<int>(type: "int", nullable: true),
                    QuotePost_Content = table.Column<string>(type: "nvarchar(777)", maxLength: 777, nullable: true),
                    RepostedPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_QuotedPostId",
                        column: x => x.QuotedPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_RepostedPostId",
                        column: x => x.RepostedPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "JoinedOn", "Username" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2022, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "user_one" },
                    { 2, new DateTimeOffset(new DateTime(2022, 7, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "user_two" },
                    { 3, new DateTimeOffset(new DateTime(2022, 7, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "user_three" },
                    { 4, new DateTimeOffset(new DateTime(2022, 7, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -3, 0, 0, 0)), "user_four" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_QuotedPostId",
                table: "Posts",
                column: "QuotedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_RepostedPostId",
                table: "Posts",
                column: "RepostedPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
