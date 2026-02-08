namespace SwissSystem.WebApp.Models;

public class ScoresView
{
    public List<PlayerScoreView> PlayerScores { get; set; } = [];
}

public class PlayerScoreView
{
    public int PlayerId { get; set; }
    public required string PlayerName { get; set; }
    public int Position { get; set; }
    public int Score { get; set; }
    public List<int> PlayersDefeated { get; set; } = [];
    public List<int> PlayersFought { get; set; } = [];
    public bool hasBye { get; set; }
    public int Buchholz { get; set; }
}
