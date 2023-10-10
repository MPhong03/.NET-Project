using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmCasts_casts_CastId",
                table: "FilmCasts");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmCasts_films_FilmId",
                table: "FilmCasts");

            migrationBuilder.DropForeignKey(
                name: "FK_films_directors_DirectorId",
                table: "films");

            migrationBuilder.DropIndex(
                name: "IX_films_DirectorId",
                table: "films");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmCasts",
                table: "FilmCasts");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "films");

            migrationBuilder.RenameTable(
                name: "FilmCasts",
                newName: "filmcasts");

            migrationBuilder.RenameIndex(
                name: "IX_FilmCasts_FilmId",
                table: "filmcasts",
                newName: "IX_filmcasts_FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmCasts_CastId",
                table: "filmcasts",
                newName: "IX_filmcasts_CastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_filmcasts",
                table: "filmcasts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "filmdirectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectorId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmdirectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_filmdirectors_directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filmdirectors_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_filmdirectors_DirectorId",
                table: "filmdirectors",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_filmdirectors_FilmId",
                table: "filmdirectors",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_filmcasts_casts_CastId",
                table: "filmcasts",
                column: "CastId",
                principalTable: "casts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_filmcasts_films_FilmId",
                table: "filmcasts",
                column: "FilmId",
                principalTable: "films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmcasts_casts_CastId",
                table: "filmcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_filmcasts_films_FilmId",
                table: "filmcasts");

            migrationBuilder.DropTable(
                name: "filmdirectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_filmcasts",
                table: "filmcasts");

            migrationBuilder.RenameTable(
                name: "filmcasts",
                newName: "FilmCasts");

            migrationBuilder.RenameIndex(
                name: "IX_filmcasts_FilmId",
                table: "FilmCasts",
                newName: "IX_FilmCasts_FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_filmcasts_CastId",
                table: "FilmCasts",
                newName: "IX_FilmCasts_CastId");

            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "films",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmCasts",
                table: "FilmCasts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_films_DirectorId",
                table: "films",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmCasts_casts_CastId",
                table: "FilmCasts",
                column: "CastId",
                principalTable: "casts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmCasts_films_FilmId",
                table: "FilmCasts",
                column: "FilmId",
                principalTable: "films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_films_directors_DirectorId",
                table: "films",
                column: "DirectorId",
                principalTable: "directors",
                principalColumn: "Id");
        }
    }
}
