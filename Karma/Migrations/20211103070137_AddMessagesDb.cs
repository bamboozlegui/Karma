using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Karma.Migrations
{
    public partial class AddMessagesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", maxLength: 450, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ToId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageId",
                table: "Messages",
                column: "MessageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
