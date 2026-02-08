using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories.Interfaces;

public interface IPlayerRepository
{
    Task<Player> AddAsync(Player player);
    Task DeleteAsync(Player player);
}