using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAndReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    reporterId = table.Column<string>(type: "text", nullable: false),
                    reportedId = table.Column<string>(type: "text", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    reviewerId = table.Column<string>(type: "text", nullable: false),
                    reviewedId = table.Column<string>(type: "text", nullable: false),
                    reviewScore = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reports_reporterId_reportedId",
                table: "reports",
                columns: new[] { "reporterId", "reportedId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_reviewerId_reviewedId",
                table: "reviews",
                columns: new[] { "reviewerId", "reviewedId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "reviews");
        }
    }
}
