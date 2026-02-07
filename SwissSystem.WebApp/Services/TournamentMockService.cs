using SwissSystem.WebApp.Models;
using SwissSystem.WebApp.Services.Interfaces;

namespace SwissSystem.WebApp.Services;

public sealed class TournamentMockService : ITournamentService
{
    private static readonly List<Tournament> Tournaments = new();
    private static int _nextTournamentId = 1;
    private static int _nextPlayerId = 1;

    static TournamentMockService()
    {
        var t1 = new Tournament
        {
            TournamentId = _nextTournamentId++,
            Name = "Campeonato Regional 2025",
            Players = new List<Player>(),
            Rounds = new List<Round>()
        };
        var t2 = new Tournament
        {
            TournamentId = _nextTournamentId++,
            Name = "Copa Local",
            Players = new List<Player>(),
            Rounds = new List<Round>()
        };
        AddDemoPlayers(t1);
        AddDemoPlayers(t2);
        Tournaments.Add(t1);
        Tournaments.Add(t2);
    }

    private static void AddDemoPlayers(Tournament tournament)
    {
        var names = new[] { "Ana Silva", "Bruno Costa", "Carlos Lima", "Diana Souza", "Eduardo Santos", "Fernanda Oliveira", "Gabriel Pereira" };
        foreach (var name in names)
        {
            tournament.Players!.Add(new Player
            {
                Id = _nextPlayerId++,
                Name = name,
                TournamentId = tournament.TournamentId
            });
        }
    }

    public Task<IReadOnlyList<Tournament>> GetAllTournamentsAsync()
    {
        var ordered = Tournaments.OrderBy(t => t.Name).ToList();
        return Task.FromResult<IReadOnlyList<Tournament>>(ordered);
    }

    public Task<Tournament?> GetTournamentWithPlayersAsync(int tournamentId)
    {
        var tournament = Tournaments.FirstOrDefault(t => t.TournamentId == tournamentId);
        return Task.FromResult(tournament);
    }

    public Task<Tournament> AddTournamentAsync(string name)
    {
        var tournament = new Tournament
        {
            TournamentId = _nextTournamentId++,
            Name = name,
            Players = new List<Player>(),
            Rounds = new List<Round>()
        };
        Tournaments.Add(tournament);
        return Task.FromResult(tournament);
    }

    public Task DeleteTournamentAsync(Tournament tournament)
    {
        Tournaments.Remove(tournament);
        return Task.CompletedTask;
    }

    public Task<Player> AddPlayerAsync(int tournamentId, string name)
    {
        var tournament = Tournaments.FirstOrDefault(t => t.TournamentId == tournamentId);
        if (tournament == null)
            throw new ArgumentException("Tournament not found.", nameof(tournamentId));

        var player = new Player
        {
            Id = _nextPlayerId++,
            Name = name,
            TournamentId = tournamentId
        };
        tournament.Players ??= new List<Player>();
        tournament.Players.Add(player);
        return Task.FromResult(player);
    }

    public Task RemovePlayerAsync(Player player)
    {
        var tournament = Tournaments.FirstOrDefault(t => t.TournamentId == player.TournamentId);
        tournament?.Players?.Remove(player);
        return Task.CompletedTask;
    }
}
