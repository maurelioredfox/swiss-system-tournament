using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services;

public interface IScoresService
{
    Task<ScoresView> GetScoresAsync(int tournamentId);
}
