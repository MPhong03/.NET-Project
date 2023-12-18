using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundUrl",
                table: "films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundUrl",
                table: "films");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "films");

            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "films");
        }
    }
}
