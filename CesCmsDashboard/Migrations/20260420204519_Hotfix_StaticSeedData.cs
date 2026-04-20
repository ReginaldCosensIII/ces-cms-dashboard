using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CesCmsDashboard.Migrations
{
    /// <inheritdoc />
    public partial class Hotfix_StaticSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Faqs",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "TechTips",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Faqs",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 4, 7, 42, 179, DateTimeKind.Utc).AddTicks(705), new DateTime(2026, 4, 20, 4, 7, 42, 179, DateTimeKind.Utc).AddTicks(929) });

            migrationBuilder.UpdateData(
                table: "TechTips",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 4, 7, 42, 185, DateTimeKind.Utc).AddTicks(7511));
        }
    }
}
