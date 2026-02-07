using SwissSystem.WebApp.Models;
using SwissSystem.WebApp.Services.Interfaces;

namespace SwissSystem.WebApp.Services;

public sealed class RoundMockService : IRoundService
{
    private static readonly Dictionary<int, List<RoundView>> RoundsByTournament = new();
    private static int _nextRoundId = 1;
    private static int _nextMatchId = 1;

    private readonly ITournamentService _tournamentService;

    public RoundMockService(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    public Task<IReadOnlyList<RoundView>> GetRoundsAsync(int tournamentId)
    {
        if (!RoundsByTournament.TryGetValue(tournamentId, out var list))
            return Task.FromResult<IReadOnlyList<RoundView>>(new List<RoundView>());

        var ordered = list.OrderBy(r => r.Id).ToList();
        return Task.FromResult<IReadOnlyList<RoundView>>(ordered);
    }

    public Task<RoundView?> GetRoundAsync(int tournamentId, int roundId)
    {
        if (!RoundsByTournament.TryGetValue(tournamentId, out var list))
            return Task.FromResult<RoundView?>(null);

        var round = list.FirstOrDefault(r => r.Id == roundId);
        return Task.FromResult(round);
    }

    public async Task<RoundView> GenerateNextRoundAsync(int tournamentId)
    {
        var tournament = await _tournamentService.GetTournamentWithPlayersAsync(tournamentId);
        if (tournament?.Players == null || tournament.Players.Count < 2)
            throw new InvalidOperationException("Tournament not found or not enough players to generate a round.");

        var players = tournament.Players.OrderBy(p => p.Name).ToList();
        var matches = new List<MatchView>();

        for (var i = 0; i < players.Count - 1; i += 2)
        {
            var p1 = players[i];
            var p2 = players[i + 1];
            matches.Add(new MatchView
            {
                Id = _nextMatchId++,
                Player1Name = p1.Name,
                Player2Name = p2.Name,
                Player1Score = 0,
                Player2Score = 0,
                Result = Result.Pending
            });
        }

        if (players.Count % 2 != 0)
        {
            matches.Add(new MatchView
            {
                Id = _nextMatchId++,
                Player1Name = players[^1].Name,
                Player2Name = "BYE",
                Player1Score = 1,
                Player2Score = 0,
                Result = Result.Player1Wins
            });
        }

        var round = new RoundView
        {
            Id = _nextRoundId++,
            Matches = matches
        };

        if (!RoundsByTournament.ContainsKey(tournamentId))
            RoundsByTournament[tournamentId] = new List<RoundView>();
        RoundsByTournament[tournamentId].Add(round);

        return round;
    }

    public Task SetMatchResultAsync(int tournamentId, int matchId, Result result)
    {
        if (!RoundsByTournament.TryGetValue(tournamentId, out var list))
            return Task.CompletedTask;

        foreach (var round in list)
        {
            var match = round.Matches.FirstOrDefault(m => m.Id == matchId);
            if (match == null) continue;

            match.Result = result;
            match.Player1Score = result == Result.Player1Wins ? 1 : result == Result.Player2Wins ? 0 : 0;
            match.Player2Score = result == Result.Player2Wins ? 1 : result == Result.Player1Wins ? 0 : 0;
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
