using Microsoft.EntityFrameworkCore.Migrations;

namespace Karma.Migrations
{
    public partial class ModifyMessagesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "FromEmail",
                table: "Messages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToEmail",
                table: "Messages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromEmail",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ToEmail",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "FromId",
                table: "Messages",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToId",
                table: "Messages",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }
    }
}
