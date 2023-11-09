using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "films",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_films_UserId",
                table: "films",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_films_users_UserId",
                table: "films",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_films_users_UserId",
                table: "films");

            migrationBuilder.DropIndex(
                name: "IX_films_UserId",
                table: "films");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "films");
        }
    }
}
