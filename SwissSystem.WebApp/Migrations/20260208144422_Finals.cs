using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SwissSystem.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class Finals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Player_Player1Id",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Player_Player2Id",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Player_WinnerId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Round_RoundId",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Tournaments_TournamentId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Round_Tournaments_TournamentId",
                table: "Round");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Round",
                table: "Round");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.RenameTable(
                name: "Round",
                newName: "Rounds");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_Round_TournamentId",
                table: "Rounds",
                newName: "IX_Rounds_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_TournamentId",
                table: "Players",
                newName: "IX_Players_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_WinnerId",
                table: "Matches",
                newName: "IX_Matches_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_RoundId",
                table: "Matches",
                newName: "IX_Matches_RoundId");

            migrationBuilder.RenameIndex(
                name: "IX_Match_Player2Id",
                table: "Matches",
                newName: "IX_Matches_Player2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Match_Player1Id",
                table: "Matches",
                newName: "IX_Matches_Player1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FinalsBrackets",
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
                    table.PrimaryKey("PK_FinalsBrackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_FinalsPlayer1Id",
                        column: x => x.FinalsPlayer1Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_FinalsPlayer2Id",
                        column: x => x.FinalsPlayer2Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_SemifinalsAPlayer1Id",
                        column: x => x.SemifinalsAPlayer1Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_SemifinalsAPlayer2Id",
                        column: x => x.SemifinalsAPlayer2Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_SemifinalsBPlayer1Id",
                        column: x => x.SemifinalsBPlayer1Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_SemifinalsBPlayer2Id",
                        column: x => x.SemifinalsBPlayer2Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalsBrackets_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "TournamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_FinalsPlayer1Id",
                table: "FinalsBrackets",
                column: "FinalsPlayer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_FinalsPlayer2Id",
                table: "FinalsBrackets",
                column: "FinalsPlayer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_SemifinalsAPlayer1Id",
                table: "FinalsBrackets",
                column: "SemifinalsAPlayer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_SemifinalsAPlayer2Id",
                table: "FinalsBrackets",
                column: "SemifinalsAPlayer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_SemifinalsBPlayer1Id",
                table: "FinalsBrackets",
                column: "SemifinalsBPlayer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_SemifinalsBPlayer2Id",
                table: "FinalsBrackets",
                column: "SemifinalsBPlayer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_TournamentId",
                table: "FinalsBrackets",
                column: "TournamentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinalsBrackets_WinnerId",
                table: "FinalsBrackets",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player1Id",
                table: "Matches",
                column: "Player1Id",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player2Id",
                table: "Matches",
                column: "Player2Id",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Rounds_RoundId",
                table: "Matches",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Tournaments_TournamentId",
                table: "Players",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Tournaments_TournamentId",
                table: "Rounds",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player2Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Rounds_RoundId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Tournaments_TournamentId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Tournaments_TournamentId",
                table: "Rounds");

            migrationBuilder.DropTable(
                name: "FinalsBrackets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rounds",
                table: "Rounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Rounds",
                newName: "Round");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Match");

            migrationBuilder.RenameIndex(
                name: "IX_Rounds_TournamentId",
                table: "Round",
                newName: "IX_Round_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_TournamentId",
                table: "Player",
                newName: "IX_Player_TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WinnerId",
                table: "Match",
                newName: "IX_Match_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_RoundId",
                table: "Match",
                newName: "IX_Match_RoundId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player2Id",
                table: "Match",
                newName: "IX_Match_Player2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player1Id",
                table: "Match",
                newName: "IX_Match_Player1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Round",
                table: "Round",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Player_Player1Id",
                table: "Match",
                column: "Player1Id",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Player_Player2Id",
                table: "Match",
                column: "Player2Id",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Player_WinnerId",
                table: "Match",
                column: "WinnerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Round_RoundId",
                table: "Match",
                column: "RoundId",
                principalTable: "Round",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Tournaments_TournamentId",
                table: "Player",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Round_Tournaments_TournamentId",
                table: "Round",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
