using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdataintousers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "Email", "FirstName", "LastLogin", "LastName", "Password", "Role", "Status", "Username" },
                values: new object[] { new Guid("b5f81d7d-49de-4508-aab5-3d598388f02f"), new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@email.com", "Admin", null, "User", "admin123", 0, 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b5f81d7d-49de-4508-aab5-3d598388f02f"));
        }
    }
}
