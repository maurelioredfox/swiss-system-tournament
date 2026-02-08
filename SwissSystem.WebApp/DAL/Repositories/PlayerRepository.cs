using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories;

public class PlayerRepository(AppDbContext dbContext) : IPlayerRepository
{
    public async Task<Player> AddAsync(Player player)
    {
        var result = await dbContext.Players.AddAsync(player);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteAsync(Player player)
    {
        dbContext.Players.Remove(player);
        await dbContext.SaveChangesAsync();
    }
}