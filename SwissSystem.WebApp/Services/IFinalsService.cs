namespace SwissSystem.WebApp.Services;

/// <summary>
/// View model for displaying the elimination bracket (names and IDs for UI).
/// </summary>
public class FinalsBracketDisplay
{
    public int BracketId { get; set; }
    public int? SemiAPlayer1Id { get; set; }
    public string? SemiAPlayer1Name { get; set; }
    public int? SemiAPlayer2Id { get; set; }
    public string? SemiAPlayer2Name { get; set; }
    public int? SemiBPlayer1Id { get; set; }
    public string? SemiBPlayer1Name { get; set; }
    public int? SemiBPlayer2Id { get; set; }
    public string? SemiBPlayer2Name { get; set; }
    public int? FinalsPlayer1Id { get; set; }
    public string? FinalsPlayer1Name { get; set; }
    public int? FinalsPlayer2Id { get; set; }
    public string? FinalsPlayer2Name { get; set; }
    public string? WinnerName { get; set; }
}

public interface IFinalsService
{
    Task<FinalsBracketDisplay?> GetBracketAsync(int tournamentId);
    Task<FinalsBracketDisplay> StartFinalsAsync(int tournamentId, IReadOnlyList<int> top4PlayerIdsInOrder);
    Task<FinalsBracketDisplay> SetSemifinalAWinnerAsync(int tournamentId, int winnerPlayerId);
    Task<FinalsBracketDisplay> SetSemifinalBWinnerAsync(int tournamentId, int winnerPlayerId);
    Task<FinalsBracketDisplay> SetChampionAsync(int tournamentId, int championPlayerId);
}
