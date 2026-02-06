using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.DAL.Repositories.Interfaces;

public interface ITournamentRepository
{
    Task<List<Tournament>> GetAllAsync();
    Task<Tournament?> GetByIdAsync(int id);
    Task<Tournament> AddAsync(Tournament entity);
    Task<Tournament> UpdateAsync(Tournament entity);
    Task DeleteAsync(Tournament entity);
}