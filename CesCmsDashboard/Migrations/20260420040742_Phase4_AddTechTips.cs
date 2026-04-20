using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CesCmsDashboard.Migrations
{
    /// <inheritdoc />
    public partial class Phase4_AddTechTips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechTips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Slug = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    VideoUrl = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechTips", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Faqs",
                columns: new[] { "Id", "Answer", "CreatedAt", "DisplayOrder", "IsPublished", "Question", "UpdatedAt" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Mock Answer", new DateTime(2026, 4, 20, 4, 7, 42, 179, DateTimeKind.Utc).AddTicks(705), 1, true, "Local Mock Q1", new DateTime(2026, 4, 20, 4, 7, 42, 179, DateTimeKind.Utc).AddTicks(929) });

            migrationBuilder.InsertData(
                table: "TechTips",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsPublished", "Slug", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { 1, null, "<p>Use a secure password manager to share credentials.</p>", new DateTime(2026, 4, 20, 4, 7, 42, 185, DateTimeKind.Utc).AddTicks(7511), false, "how-to-securely-share-passwords", "How to securely share passwords", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_TechTips_Slug",
                table: "TechTips",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechTips");

            migrationBuilder.DeleteData(
                table: "Faqs",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
