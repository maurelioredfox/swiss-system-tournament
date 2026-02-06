using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services;

public interface ITournamentService
{
    Task<IReadOnlyList<Tournament>> GetAllTournamentsAsync();
    Task<Tournament?> GetTournamentWithPlayersAsync(int tournamentId);
    Task<Tournament> AddTournamentAsync(string name);
    Task DeleteTournamentAsync(Tournament tournament);
    Task<Player> AddPlayerAsync(int tournamentId, string name);
    Task RemovePlayerAsync(Player player);
}
