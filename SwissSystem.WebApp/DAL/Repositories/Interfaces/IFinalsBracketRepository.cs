using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories.Interfaces;

public interface IFinalsBracketRepository
{
    Task<FinalsBracket?> GetFromTournamentIdAsync(int tournamentId);
    Task<FinalsBracket> InsertAsync(FinalsBracket newBracket);
    Task<FinalsBracket> UpdateAsync(FinalsBracket bracket);
}