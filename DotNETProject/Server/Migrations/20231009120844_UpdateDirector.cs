using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "films",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "directors",
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
                    table.PrimaryKey("PK_directors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_DirectorId",
                table: "films",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_films_directors_DirectorId",
                table: "films",
                column: "DirectorId",
                principalTable: "directors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_films_directors_DirectorId",
                table: "films");

            migrationBuilder.DropTable(
                name: "directors");

            migrationBuilder.DropIndex(
                name: "IX_films_DirectorId",
                table: "films");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "films");
        }
    }
}
