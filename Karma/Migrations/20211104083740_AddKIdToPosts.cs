using Microsoft.EntityFrameworkCore.Migrations;

namespace Karma.Migrations
{
    public partial class AddKIdToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_KarmaUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_KarmaUserId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "KarmaUserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KarmaUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_KarmaUserId",
                table: "Items",
                column: "KarmaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_KarmaUserId",
                table: "Requests",
                column: "KarmaUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_KarmaUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_KarmaUserId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "KarmaUserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "KarmaUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
