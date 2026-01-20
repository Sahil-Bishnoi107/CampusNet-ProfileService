using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewAndReport2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reviewedName",
                table: "reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reportedName",
                table: "reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reviewedName",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "reportedName",
                table: "reports");
        }
    }
}
