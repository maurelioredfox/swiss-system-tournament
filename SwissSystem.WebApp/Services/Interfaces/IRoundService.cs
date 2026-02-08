using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services.Interfaces;

public interface IRoundService
{
    Task<IReadOnlyList<RoundView>> GetRoundsAsync(int tournamentId);
    Task<RoundView?> GetRoundAsync(int tournamentId, int roundId);
    Task<RoundView> GenerateNextRoundAsync(int tournamentId);
    Task SetMatchResultAsync(int tournamentId, int matchId, Result result);
    Task DeleteLastRoundAsync(int tournamentId);
}
