using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AStar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileAccessDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastViewed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SoftDeletePending = table.Column<bool>(type: "bit", nullable: false),
                    MoveRequired = table.Column<bool>(type: "bit", nullable: false),
                    HardDeletePending = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAccessDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagsToIgnore",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsToIgnore", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "TagsToIgnoreCompletely",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsToIgnoreCompletely", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    FileAccessDetailId = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => new { x.FileName, x.DirectoryName });
                    table.ForeignKey(
                        name: "FK_Files_FileAccessDetails_FileAccessDetailId",
                        column: x => x.FileAccessDetailId,
                        principalTable: "FileAccessDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileAccessDetailId",
                table: "Files",
                column: "FileAccessDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "TagsToIgnore");

            migrationBuilder.DropTable(
                name: "TagsToIgnoreCompletely");

            migrationBuilder.DropTable(
                name: "FileAccessDetails");
        }
    }
}
