using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nation",
                table: "films");

            migrationBuilder.AddColumn<int>(
                name: "NationId",
                table: "films",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "nations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_NationId",
                table: "films",
                column: "NationId");

            migrationBuilder.AddForeignKey(
                name: "FK_films_nations_NationId",
                table: "films",
                column: "NationId",
                principalTable: "nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_films_nations_NationId",
                table: "films");

            migrationBuilder.DropTable(
                name: "nations");

            migrationBuilder.DropIndex(
                name: "IX_films_NationId",
                table: "films");

            migrationBuilder.DropColumn(
                name: "NationId",
                table: "films");

            migrationBuilder.AddColumn<string>(
                name: "Nation",
                table: "films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
