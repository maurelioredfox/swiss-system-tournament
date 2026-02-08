using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories;

public class RoundRepository(AppDbContext dbContext) : IRoundRepository
{
    public async Task<IList<Round>> GetByTournamentIdAsync(int tournamentId)
    {
        var result = await dbContext.Rounds
            .Where(r => r.TournamentId == tournamentId)
            .Include(r=>r.Matches)
                .ThenInclude(m=> m.Player1)
            .Include(r=>r.Matches)
                .ThenInclude(m=> m.Player2)
            .Include(r=>r.Matches)
                .ThenInclude(m=> m.Winner)
            .ToListAsync();
        return  result;
    }

    public async Task<Round?> GetByIdAsync(int id)
    {
        var result = await dbContext.Rounds
            .Where(r => r.Id == id)
            .Include(r=>r.Matches)
                .ThenInclude(m=> m.Player1)
            .Include(r=>r.Matches)
                .ThenInclude(m=> m.Player2)
            .Include(r=>r.Matches)
                .ThenInclude(m=> m.Winner)
            .FirstOrDefaultAsync();
        return  result;
    }

    public async Task<Round> AddAsync(Round round)
    {
        var result = await dbContext.Rounds.AddAsync(round);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task RemoveAsync(Round round)
    {
        dbContext.Rounds.Remove(round);
        await dbContext.SaveChangesAsync();
    }
}