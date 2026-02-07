using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SwissSystem.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class finals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinalsBracket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TournamentId = table.Column<int>(type: "integer", nullable: false),
                    SemifinalsAPlayer1Id = table.Column<int>(type: "integer", nullable: true),
                    SemifinalsAPlayer2Id = table.Column<int>(type: "integer", nullable: true),
                    SemifinalsBPlayer1Id = table.Column<int>(type: "integer", nullable: true),
                    SemifinalsBPlayer2Id = table.Column<int>(type: "integer", nullable: true),
                    FinalsPlayer1Id = table.Column<int>(type: "integer", nullable: true),
                    FinalsPlayer2Id = table.Column<int>(type: "integer", nullable: true),
                    WinnerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalsBracket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_FinalsPlayer1Id",
                        column: x => x.FinalsPlayer1Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_FinalsPlayer2Id",
                        column: x => x.FinalsPlayer2Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_SemifinalsAPlayer1Id",
                        column: x => x.SemifinalsAPlayer1Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_SemifinalsAPlayer2Id",
                        column: x => x.SemifinalsAPlayer2Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_SemifinalsBPlayer1Id",
                        column: x => x.SemifinalsBPlayer1Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_SemifinalsBPlayer2Id",
                        column: x => x.SemifinalsBPlayer2Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Player_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBracket_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_FinalsPlayer1Id",
                table: "FinalsBracket",
                column: "FinalsPlayer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_FinalsPlayer2Id",
                table: "FinalsBracket",
                column: "FinalsPlayer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_SemifinalsAPlayer1Id",
                table: "FinalsBracket",
                column: "SemifinalsAPlayer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_SemifinalsAPlayer2Id",
                table: "FinalsBracket",
                column: "SemifinalsAPlayer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_SemifinalsBPlayer1Id",
                table: "FinalsBracket",
                column: "SemifinalsBPlayer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_SemifinalsBPlayer2Id",
                table: "FinalsBracket",
                column: "SemifinalsBPlayer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_TournamentId",
                table: "FinalsBracket",
                column: "TournamentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBracket_WinnerId",
                table: "FinalsBracket",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalsBracket");
        }
    }
}
