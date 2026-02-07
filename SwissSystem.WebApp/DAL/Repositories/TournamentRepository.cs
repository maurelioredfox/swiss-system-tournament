using Microsoft.EntityFrameworkCore;
using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories;

public class TournamentRepository(AppDbContext dbContext) : ITournamentRepository
{
    public async Task<List<Tournament>> GetAllAsync()
    {
        return await dbContext.Tournaments.ToListAsync();
    }

    public async Task<Tournament?> GetByIdAsync(int id)
    {
        return await dbContext.Tournaments
            .Include(t => t.Players)
            .Include(t=>t.Rounds)
            .FirstOrDefaultAsync(t => t.TournamentId == id);
    }

    public async Task<Tournament> AddAsync(Tournament entity)
    {
        var result = await dbContext.Tournaments.AddAsync(entity);
        return result.Entity;
    }

    public async Task<Tournament> UpdateAsync(Tournament entity)
    {
        var result = dbContext.Tournaments.Update(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteAsync(Tournament entity)
    {
        dbContext.Tournaments.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}