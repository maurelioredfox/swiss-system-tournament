using Microsoft.EntityFrameworkCore;
using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories;

public class FinalsBracketRepository(AppDbContext dbContext) : IFinalsBracketRepository
{
    public async Task<FinalsBracket?> GetFromTournamentIdAsync(int tournamentId)
    {
        return await dbContext.FinalsBrackets
            .Where(f => f.TournamentId == tournamentId)
            .Include(f => f.Tournament)
            .Include(f => f.SemifinalsAPlayer1)
            .Include(f => f.SemifinalsAPlayer2)
            .Include(f => f.SemifinalsBPlayer1)
            .Include(f => f.SemifinalsBPlayer2)
            .Include(f => f.FinalsPlayer1)
            .Include(f => f.FinalsPlayer2)
            .Include(f => f.Winner)
            .FirstOrDefaultAsync();
    }

    public async Task<FinalsBracket> InsertAsync(FinalsBracket newBracket)
    {
        var result = await dbContext.FinalsBrackets.AddAsync(newBracket);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<FinalsBracket> UpdateAsync(FinalsBracket bracket)
    {
        dbContext.FinalsBrackets.Update(bracket);
        await dbContext.SaveChangesAsync();
        return bracket;
    }
}