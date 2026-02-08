using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;
using SwissSystem.WebApp.Services.Interfaces;

namespace SwissSystem.WebApp.Services;

public class TournamentService(
    ITournamentRepository tournamentRepository,
    IPlayerRepository playerRepository
) : ITournamentService
{
    public async Task<IReadOnlyList<Tournament>> GetAllTournamentsAsync()
    {
        return await tournamentRepository.GetAllAsync();
    }

    public async Task<Tournament?> GetTournamentWithPlayersAsync(int tournamentId)
    {
        return await tournamentRepository.GetByIdAsync(tournamentId);
    }

    public async Task<Tournament> AddTournamentAsync(string name)
    {
        var tournament = new Tournament { Name = name };
        return await tournamentRepository.AddAsync(tournament);
    }

    public Task DeleteTournamentAsync(Tournament tournament)
    {
        return tournamentRepository.DeleteAsync(tournament);
    }

    public async Task<Player> AddPlayerAsync(int tournamentId, string name)
    {
        var player = new Player { TournamentId = tournamentId, Name = name };
        return await playerRepository.AddAsync(player);
    }

    public async Task RemovePlayerAsync(Player player)
    {
        await playerRepository.DeleteAsync(player);
    }
}