using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CesCmsDashboard.Migrations
{
    /// <inheritdoc />
    public partial class StandardizeTechTipIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TechTips",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TechTips",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "TechTips",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            // 1. Drop the primary key constraint first
            migrationBuilder.DropPrimaryKey(
                name: "PK_TechTips",
                table: "TechTips");

            // 2. Drop the stubborn integer column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "TechTips");

            // 3. Add the brand new Guid column
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TechTips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // 4. Re-establish the Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_TechTips",
                table: "TechTips",
                column: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Faqs",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "TechTips",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsPublished", "Slug", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), null, "<p>Use a secure password manager to share credentials.</p>", new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "how-to-securely-share-passwords", "How to securely share passwords", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TechTips",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TechTips",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "TechTips",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TechTips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Faqs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.InsertData(
                table: "TechTips",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "IsPublished", "Slug", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { 1, null, "<p>Use a secure password manager to share credentials.</p>", new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Utc), false, "how-to-securely-share-passwords", "How to securely share passwords", null, null });
        }
    }
}
