using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpisodeNumber",
                table: "episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeNumber",
                table: "episodes");
        }
    }
}
