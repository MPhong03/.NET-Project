using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerId",
                table: "films",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maximum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_BannerId",
                table: "films",
                column: "BannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_films_banners_BannerId",
                table: "films",
                column: "BannerId",
                principalTable: "banners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_films_banners_BannerId",
                table: "films");

            migrationBuilder.DropTable(
                name: "banners");

            migrationBuilder.DropIndex(
                name: "IX_films_BannerId",
                table: "films");

            migrationBuilder.DropColumn(
                name: "BannerId",
                table: "films");
        }
    }
}
