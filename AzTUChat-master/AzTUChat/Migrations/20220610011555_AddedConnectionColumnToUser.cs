using Microsoft.EntityFrameworkCore.Migrations;

namespace AzTUChat.Migrations
{
    public partial class AddedConnectionColumnToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserImages_AppUserId",
                table: "UserImages");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_AppUserId",
                table: "UserImages",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserImages_AppUserId",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_AppUserId",
                table: "UserImages",
                column: "AppUserId");
        }
    }
}
