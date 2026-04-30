using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CesCmsDashboard.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSlugFromTechTips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TechTips_Slug",
                table: "TechTips");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "TechTips");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "TechTips",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TechTips",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Slug",
                value: "how-to-securely-share-passwords");

            migrationBuilder.CreateIndex(
                name: "IX_TechTips_Slug",
                table: "TechTips",
                column: "Slug",
                unique: true);
        }
    }
}
