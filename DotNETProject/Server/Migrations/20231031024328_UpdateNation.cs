using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nation",
                table: "films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nation",
                table: "films");
        }
    }
}
