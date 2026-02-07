namespace SwissSystem.WebApp.Models;

public class RoundView
{
    public int Id { get; set; }
    
    public required List<MatchView> Matches { get; set; }
       
}

public class MatchView
{
    public int Id { get; set; }
    
    public required string Player1Name { get; set; }
    public required string Player2Name { get; set; }
    
    public int Player1Score { get; set; }
    public int Player2Score { get; set; }
    
    public Result Result { get; set; }
}

public enum Result
{
    Pending,
    Player1Wins,
    Player2Wins,
}