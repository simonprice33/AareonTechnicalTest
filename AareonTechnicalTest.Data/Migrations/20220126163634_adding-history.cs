using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AareonTechnicalTest.Migrations
{
    public partial class addinghistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RowId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TableName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Changed = table.Column<string>(type: "TEXT", nullable: true),
                    Kind = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoHistory");
        }
    }
}
