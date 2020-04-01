using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UTTAF.API.Migrations
{
    public partial class InitRobotSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SessionReference = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Robots",
                columns: table => new
                {
                    RobotId = table.Column<Guid>(nullable: false),
                    SessionReference = table.Column<string>(nullable: false),
                    RobotStatus = table.Column<int>(nullable: false),
                    DataOperation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robots", x => x.RobotId);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionReference = table.Column<string>(nullable: false),
                    SessionStatus = table.Column<int>(nullable: false),
                    SessionDate = table.Column<DateTime>(nullable: true),
                    SessionPassword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionReference);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Robots");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
