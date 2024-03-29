using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlterOTPAddOTPUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Usage",
                table: "OTPs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usage",
                table: "OTPs");
        }
    }
}
