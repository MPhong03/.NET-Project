using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFilmV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_films_advertises_AdvertiseId",
                table: "films");

            migrationBuilder.DropTable(
                name: "advertises");

            migrationBuilder.DropIndex(
                name: "IX_films_AdvertiseId",
                table: "films");

            migrationBuilder.DropColumn(
                name: "AdvertiseId",
                table: "films");

            migrationBuilder.AddColumn<bool>(
                name: "isActiveBanner",
                table: "films",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActiveBanner",
                table: "films");

            migrationBuilder.AddColumn<int>(
                name: "AdvertiseId",
                table: "films",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "advertises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maximum = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertises", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_AdvertiseId",
                table: "films",
                column: "AdvertiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_films_advertises_AdvertiseId",
                table: "films",
                column: "AdvertiseId",
                principalTable: "advertises",
                principalColumn: "Id");
        }
    }
}
