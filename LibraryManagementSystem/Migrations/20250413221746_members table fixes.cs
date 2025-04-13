using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.API.Migrations
{
    /// <inheritdoc />
    public partial class memberstablefixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MmebershipDate",
                table: "Members",
                newName: "MembershipStartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MembershipStartDate",
                table: "Members",
                newName: "MmebershipDate");
        }
    }
}
