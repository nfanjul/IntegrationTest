using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationTest.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("42d4b810-8c35-4692-ac8e-f4082dcba41a"), "Diego Mariño" },
                    { new Guid("779ed204-64b6-472b-8fd3-0b726cab904f"), "Manu García" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("68bd77b6-4528-4db3-abf2-a3a4370c5e1b"), "Real Sporting de Gijón" },
                    { new Guid("a5a855b0-d575-42da-8466-cb10b9324ff0"), "Real Betis" },
                    { new Guid("a726f480-862c-4b58-a4eb-3f4f6cde6926"), "UP Langreo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
