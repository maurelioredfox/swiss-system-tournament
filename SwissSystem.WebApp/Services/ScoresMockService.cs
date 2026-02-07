using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services;

public sealed class ScoresMockService : IScoresService
{
    private readonly ITournamentService _tournamentService;
    private readonly IRoundService _roundService;

    public ScoresMockService(ITournamentService tournamentService, IRoundService roundService)
    {
        _tournamentService = tournamentService;
        _roundService = roundService;
    }

    public async Task<ScoresView> GetScoresAsync(int tournamentId)
    {
        var tournament = await _tournamentService.GetTournamentWithPlayersAsync(tournamentId);
        var rounds = await _roundService.GetRoundsAsync(tournamentId);

        if (tournament?.Players == null || tournament.Players.Count == 0)
            return new ScoresView { PlayerScores = new List<PlayerScoreView>() };

        var nameToPlayer = tournament.Players.ToDictionary(p => p.Name, p => p);
        var scoreByPlayerId = new Dictionary<int, int>();
        var opponentsByPlayerId = new Dictionary<int, List<int>>();

        foreach (var p in tournament.Players)
        {
            scoreByPlayerId[p.Id] = 0;
            opponentsByPlayerId[p.Id] = new List<int>();
        }

        foreach (var round in rounds)
        {
            foreach (var m in round.Matches.Where(x => x.Player2Name != "BYE"))
            {
                if (!nameToPlayer.TryGetValue(m.Player1Name, out var p1) || !nameToPlayer.TryGetValue(m.Player2Name, out var p2))
                    continue;
                opponentsByPlayerId[p1.Id].Add(p2.Id);
                opponentsByPlayerId[p2.Id].Add(p1.Id);
                if (m.Result == Result.Player1Wins)
                    scoreByPlayerId[p1.Id]++;
                else if (m.Result == Result.Player2Wins)
                    scoreByPlayerId[p2.Id]++;
            }
            // BYE: winner gets a win
            foreach (var m in round.Matches.Where(x => x.Player2Name == "BYE"))
            {
                if (nameToPlayer.TryGetValue(m.Player1Name, out var p))
                    scoreByPlayerId[p.Id]++;
            }
        }

        var buchholzByPlayerId = new Dictionary<int, int>();
        foreach (var pid in scoreByPlayerId.Keys)
        {
            var buchholz = opponentsByPlayerId[pid].Sum(oppId => scoreByPlayerId.TryGetValue(oppId, out var s) ? s : 0);
            buchholzByPlayerId[pid] = buchholz;
        }

        var list = tournament.Players
            .Select(p => new PlayerScoreView
            {
                PlayerId = p.Id,
                PlayerName = p.Name,
                Score = scoreByPlayerId.TryGetValue(p.Id, out var s) ? s : 0,
                Buchholz = buchholzByPlayerId.TryGetValue(p.Id, out var b) ? b : 0,
                PlayersDefeated = new List<int>(),
                Position = 0
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Buchholz)
            .ThenBy(x => x.PlayerName)
            .ToList();

        for (var i = 0; i < list.Count; i++)
            list[i].Position = i + 1;

        return new ScoresView { PlayerScores = list };
    }
}
