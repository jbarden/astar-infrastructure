using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AStar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileAccessDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetailsLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastViewed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SoftDeletePending = table.Column<bool>(type: "bit", nullable: false),
                    MoveRequired = table.Column<bool>(type: "bit", nullable: false),
                    HardDeletePending = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_FileAccessDetails", x => x.Id));

            migrationBuilder.CreateTable(
                name: "ModelsToIgnore",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_ModelsToIgnore", x => x.Value));

            migrationBuilder.CreateTable(
                name: "ScrapeDirectories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RootDirectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseSaveDirectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseDirectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseDirectoryFamous = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_ScrapeDirectories", x => x.Id));

            migrationBuilder.CreateTable(
                name: "SearchConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopWallpapers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchStringPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchStringSuffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subscriptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePauseInSeconds = table.Column<int>(type: "int", nullable: false),
                    StartingPageNumber = table.Column<int>(type: "int", nullable: false),
                    TotalPages = table.Column<int>(type: "int", nullable: false),
                    UseHeadless = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionsStartingPageNumber = table.Column<int>(type: "int", nullable: false),
                    SubscriptionsTotalPages = table.Column<int>(type: "int", nullable: false),
                    TopWallpapersTotalPages = table.Column<int>(type: "int", nullable: false),
                    TopWallpapersStartingPageNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_SearchConfiguration", x => x.Id));

            migrationBuilder.CreateTable(
                name: "TagsToIgnore",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IgnoreImage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_TagsToIgnore", x => x.Value));

            migrationBuilder.CreateTable(
                name: "UserConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_UserConfiguration", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileAccessDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_FileAccessDetails_FileAccessDetailId",
                        column: x => x.FileAccessDetailId,
                        principalTable: "FileAccessDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastKnownImageCount = table.Column<int>(type: "int", nullable: false),
                    LastPageVisited = table.Column<int>(type: "int", nullable: false),
                    TotalPages = table.Column<int>(type: "int", nullable: false),
                    SearchConfigurationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchCategory_SearchConfiguration_SearchConfigurationId",
                        column: x => x.SearchConfigurationId,
                        principalTable: "SearchConfiguration",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScrapeConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserConfigurationId = table.Column<int>(type: "int", nullable: false),
                    SearchConfigurationId = table.Column<int>(type: "int", nullable: false),
                    ScrapeDirectoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapeConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScrapeConfiguration_ScrapeDirectories_ScrapeDirectoriesId",
                        column: x => x.ScrapeDirectoriesId,
                        principalTable: "ScrapeDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScrapeConfiguration_SearchConfiguration_SearchConfigurationId",
                        column: x => x.SearchConfigurationId,
                        principalTable: "SearchConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScrapeConfiguration_UserConfiguration_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileAccessDetailId",
                table: "Files",
                column: "FileAccessDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrapeConfiguration_ScrapeDirectoriesId",
                table: "ScrapeConfiguration",
                column: "ScrapeDirectoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrapeConfiguration_SearchConfigurationId",
                table: "ScrapeConfiguration",
                column: "SearchConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrapeConfiguration_UserConfigurationId",
                table: "ScrapeConfiguration",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchCategory_SearchConfigurationId",
                table: "SearchCategory",
                column: "SearchConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "ModelsToIgnore");

            migrationBuilder.DropTable(
                name: "ScrapeConfiguration");

            migrationBuilder.DropTable(
                name: "SearchCategory");

            migrationBuilder.DropTable(
                name: "TagsToIgnore");

            migrationBuilder.DropTable(
                name: "FileAccessDetails");

            migrationBuilder.DropTable(
                name: "ScrapeDirectories");

            migrationBuilder.DropTable(
                name: "UserConfiguration");

            migrationBuilder.DropTable(
                name: "SearchConfiguration");
        }
    }
}
