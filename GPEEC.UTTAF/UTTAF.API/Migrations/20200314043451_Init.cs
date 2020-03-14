using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTTAF.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auths",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(nullable: false),
                    SessionName = table.Column<string>(nullable: false),
                    SessionPassword = table.Column<string>(nullable: false),
                    SessionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auths", x => x.SessionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auths");
        }
    }
}
