using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episodes_tvseries_TVSeriesId",
                table: "episodes");

            migrationBuilder.DropIndex(
                name: "IX_episodes_TVSeriesId",
                table: "episodes");

            migrationBuilder.DropColumn(
                name: "TVSeriesId",
                table: "episodes");

            migrationBuilder.AddColumn<string>(
                name: "TrailerUrl",
                table: "films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "casts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmCasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    CastId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmCasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmCasts_casts_CastId",
                        column: x => x.CastId,
                        principalTable: "casts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmCasts_films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_episodes_SeriesId",
                table: "episodes",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmCasts_CastId",
                table: "FilmCasts",
                column: "CastId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmCasts_FilmId",
                table: "FilmCasts",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_episodes_tvseries_SeriesId",
                table: "episodes",
                column: "SeriesId",
                principalTable: "tvseries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episodes_tvseries_SeriesId",
                table: "episodes");

            migrationBuilder.DropTable(
                name: "FilmCasts");

            migrationBuilder.DropTable(
                name: "casts");

            migrationBuilder.DropIndex(
                name: "IX_episodes_SeriesId",
                table: "episodes");

            migrationBuilder.DropColumn(
                name: "TrailerUrl",
                table: "films");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "episodes");

            migrationBuilder.AddColumn<int>(
                name: "TVSeriesId",
                table: "episodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_episodes_TVSeriesId",
                table: "episodes",
                column: "TVSeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_episodes_tvseries_TVSeriesId",
                table: "episodes",
                column: "TVSeriesId",
                principalTable: "tvseries",
                principalColumn: "Id");
        }
    }
}
