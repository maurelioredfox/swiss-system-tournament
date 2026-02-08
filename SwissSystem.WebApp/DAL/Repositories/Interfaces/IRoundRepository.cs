using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories.Interfaces;

public interface IRoundRepository
{
    Task<IList<Round>> GetByTournamentIdAsync(int tournamentId);
    Task<Round?> GetByIdAsync(int id);
    Task<Round> AddAsync(Round round);
    Task RemoveAsync(Round round);
}