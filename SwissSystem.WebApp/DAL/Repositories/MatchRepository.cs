using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories;

public class MatchRepository(AppDbContext dbContext) : IMatchRepository
{
    public async Task<Match?> GetByIdAsync(int matchId)
    {
        return await dbContext.Matches.FindAsync(matchId);
    }

    public async Task<Match> UpdateAsync(Match match)
    {
        var result = dbContext.Matches.Update(match);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }
}