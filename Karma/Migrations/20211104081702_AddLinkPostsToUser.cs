using Microsoft.EntityFrameworkCore.Migrations;

namespace Karma.Migrations
{
    public partial class AddLinkPostsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KarmaUserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KarmaUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_KarmaUserId",
                table: "Requests",
                column: "KarmaUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_KarmaUserId",
                table: "Items",
                column: "KarmaUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_KarmaUserId",
                table: "Items",
                column: "KarmaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_KarmaUserId",
                table: "Requests",
                column: "KarmaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_KarmaUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_KarmaUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_KarmaUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Items_KarmaUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "KarmaUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "KarmaUserId",
                table: "Items");
        }
    }
}
