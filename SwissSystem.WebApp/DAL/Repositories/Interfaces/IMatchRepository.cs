using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories.Interfaces;

public interface IMatchRepository
{
    Task<Match?> GetByIdAsync(int matchId);
    Task<Match> UpdateAsync(Match match);
}