using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services.Interfaces;

public interface IFinalsService
{
    Task<FinalsBracket?> GetBracketAsync(int tournamentId);
    Task<FinalsBracket> StartFinalsAsync(int tournamentId, IReadOnlyList<int> top4PlayerIdsInOrder);
    Task<FinalsBracket> SetSemifinalAWinnerAsync(int tournamentId, int winnerPlayerId);
    Task<FinalsBracket> SetSemifinalBWinnerAsync(int tournamentId, int winnerPlayerId);
    Task<FinalsBracket> SetChampionAsync(int tournamentId, int championPlayerId);
}
