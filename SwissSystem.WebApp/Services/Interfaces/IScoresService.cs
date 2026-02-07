using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services.Interfaces;

public interface IScoresService
{
    Task<ScoresView> GetScoresAsync(int tournamentId);
}
