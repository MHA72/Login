using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Login.Migrations
{
    /// <inheritdoc />
    public partial class sheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileSheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSheets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SheetId = table.Column<string>(type: "TEXT", nullable: false),
                    SheetName = table.Column<string>(type: "TEXT", nullable: false),
                    FileSheetId = table.Column<Guid>(type: "TEXT", nullable: true),
                    InsertTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheets_FileSheets_FileSheetId",
                        column: x => x.FileSheetId,
                        principalTable: "FileSheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileSheets_UserId",
                table: "FileSheets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheets_FileSheetId",
                table: "Sheets",
                column: "FileSheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sheets");

            migrationBuilder.DropTable(
                name: "FileSheets");
        }
    }
}
