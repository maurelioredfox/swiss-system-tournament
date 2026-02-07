using SwissSystem.WebApp.Models;

namespace SwissSystem.WebApp.Services;

public sealed class FinalsMockService : IFinalsService
{
    private static readonly Dictionary<int, FinalsBracketStored> BracketsByTournament = new();
    private static int _nextBracketId = 1;

    private readonly ITournamentService _tournamentService;

    public FinalsMockService(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    public Task<FinalsBracketDisplay?> GetBracketAsync(int tournamentId)
    {
        if (!BracketsByTournament.TryGetValue(tournamentId, out var stored))
            return Task.FromResult<FinalsBracketDisplay?>(null);
        var display = ToDisplay(stored);
        return Task.FromResult<FinalsBracketDisplay?>(display);
    }

    public async Task<FinalsBracketDisplay> StartFinalsAsync(int tournamentId, IReadOnlyList<int> top4PlayerIdsInOrder)
    {
        if (top4PlayerIdsInOrder.Count != 4)
            throw new ArgumentException("Exactly 4 player IDs required.", nameof(top4PlayerIdsInOrder));

        var tournament = await _tournamentService.GetTournamentWithPlayersAsync(tournamentId);
        if (tournament?.Players == null)
            throw new InvalidOperationException("Tournament not found.");

        var idToName = tournament.Players.ToDictionary(p => p.Id, p => p.Name);

        // Top 4 in order: 1st, 2nd, 3rd, 4th
        // Semi A: 1st vs 4th. Semi B: 2nd vs 3rd.
        var p1 = top4PlayerIdsInOrder[0];
        var p2 = top4PlayerIdsInOrder[1];
        var p3 = top4PlayerIdsInOrder[2];
        var p4 = top4PlayerIdsInOrder[3];

        var stored = new FinalsBracketStored
        {
            Id = _nextBracketId++,
            TournamentId = tournamentId,
            SemifinalsAPlayer1Id = p1,
            SemifinalsAPlayer2Id = p4,
            SemifinalsBPlayer1Id = p2,
            SemifinalsBPlayer2Id = p3,
            SemifinalAWinnerId = null,
            SemifinalBWinnerId = null,
            WinnerId = null,
            IdToName = idToName
        };
        BracketsByTournament[tournamentId] = stored;
        return ToDisplay(stored);
    }

    private static FinalsBracketDisplay ToDisplay(FinalsBracketStored s)
    {
        string? GetName(int? id) => id.HasValue && s.IdToName.TryGetValue(id.Value, out var n) ? n : null;
        return new FinalsBracketDisplay
        {
            BracketId = s.Id,
            SemiAPlayer1Id = s.SemifinalsAPlayer1Id,
            SemiAPlayer1Name = GetName(s.SemifinalsAPlayer1Id),
            SemiAPlayer2Id = s.SemifinalsAPlayer2Id,
            SemiAPlayer2Name = GetName(s.SemifinalsAPlayer2Id),
            SemiBPlayer1Id = s.SemifinalsBPlayer1Id,
            SemiBPlayer1Name = GetName(s.SemifinalsBPlayer1Id),
            SemiBPlayer2Id = s.SemifinalsBPlayer2Id,
            SemiBPlayer2Name = GetName(s.SemifinalsBPlayer2Id),
            FinalsPlayer1Id = s.SemifinalAWinnerId,
            FinalsPlayer1Name = GetName(s.SemifinalAWinnerId),
            FinalsPlayer2Id = s.SemifinalBWinnerId,
            FinalsPlayer2Name = GetName(s.SemifinalBWinnerId),
            WinnerName = GetName(s.WinnerId)
        };
    }

    public Task<FinalsBracketDisplay> SetSemifinalAWinnerAsync(int tournamentId, int winnerPlayerId)
    {
        if (!BracketsByTournament.TryGetValue(tournamentId, out var stored))
            throw new InvalidOperationException("Bracket not found.");
        if (winnerPlayerId != stored.SemifinalsAPlayer1Id && winnerPlayerId != stored.SemifinalsAPlayer2Id)
            throw new ArgumentException("Player is not in Semifinal A.", nameof(winnerPlayerId));
        stored.SemifinalAWinnerId = winnerPlayerId;
        return Task.FromResult(ToDisplay(stored));
    }

    public Task<FinalsBracketDisplay> SetSemifinalBWinnerAsync(int tournamentId, int winnerPlayerId)
    {
        if (!BracketsByTournament.TryGetValue(tournamentId, out var stored))
            throw new InvalidOperationException("Bracket not found.");
        if (winnerPlayerId != stored.SemifinalsBPlayer1Id && winnerPlayerId != stored.SemifinalsBPlayer2Id)
            throw new ArgumentException("Player is not in Semifinal B.", nameof(winnerPlayerId));
        stored.SemifinalBWinnerId = winnerPlayerId;
        return Task.FromResult(ToDisplay(stored));
    }

    public Task<FinalsBracketDisplay> SetChampionAsync(int tournamentId, int championPlayerId)
    {
        if (!BracketsByTournament.TryGetValue(tournamentId, out var stored))
            throw new InvalidOperationException("Bracket not found.");
        if (championPlayerId != stored.SemifinalAWinnerId && championPlayerId != stored.SemifinalBWinnerId)
            throw new ArgumentException("Player is not in the Finals.", nameof(championPlayerId));
        stored.WinnerId = championPlayerId;
        return Task.FromResult(ToDisplay(stored));
    }

    private class FinalsBracketStored
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int? SemifinalsAPlayer1Id { get; set; }
        public int? SemifinalsAPlayer2Id { get; set; }
        public int? SemifinalsBPlayer1Id { get; set; }
        public int? SemifinalsBPlayer2Id { get; set; }
        public int? SemifinalAWinnerId { get; set; }
        public int? SemifinalBWinnerId { get; set; }
        public int? WinnerId { get; set; }
        public Dictionary<int, string> IdToName { get; set; } = new();
    }
}
