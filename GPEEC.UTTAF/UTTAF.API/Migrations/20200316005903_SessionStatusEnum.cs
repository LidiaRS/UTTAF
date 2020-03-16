using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTTAF.API.Migrations
{
    public partial class SessionStatusEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auths",
                columns: table => new
                {
                    SessionReference = table.Column<string>(nullable: false),
                    SessionStatus = table.Column<int>(nullable: false),
                    SessionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auths", x => x.SessionReference);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auths");
        }
    }
}
