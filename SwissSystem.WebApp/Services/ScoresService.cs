using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;
using SwissSystem.WebApp.Services.Interfaces;

namespace SwissSystem.WebApp.Services;

public class ScoresService(
    ITournamentRepository tournamentRepository,
    IRoundRepository roundRepository,
    IMatchRepository matchRepository
) : IScoresService
{
    public async Task<ScoresView> GetScoresAsync(int tournamentId)
    {
        var players = (await tournamentRepository.GetByIdAsync(tournamentId))?.Players;
        var rounds = await roundRepository.GetByTournamentIdAsync(tournamentId);
        
        if (players == null)
            throw new BadHttpRequestException("Players not found, check tournament");

        var response = new ScoresView();
        
        foreach (var player in players)
        {
            var playerMatches = rounds
                .SelectMany(r => r.Matches)
                .Where(m => m.Player1Id == player.Id || m.Player2Id == player.Id)
                .ToList();
            
            var wins = playerMatches.Where(m => m.WinnerId == player.Id).ToList();
            var byes = playerMatches.Count(m => m.Bye);

            var scoreView = new PlayerScoreView()
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                Score = ((wins.Count + byes) * 3),
                hasBye = byes > 0,
                PlayersFought = playerMatches.Where(m => !m.Bye).Select(match => 
                    match.Player1Id == player.Id ? match.Player2Id!.Value : match.Player1Id
                ).ToList(),
                PlayersDefeated = wins.Select(match =>
                    match.WinnerId!.Value == match.Player1Id ? match.Player2Id!.Value : match.Player1Id
                ).ToList()
            };
            
            response.PlayerScores.Add(scoreView);
        }
            
        //calculate Buchholz
        foreach (var score in response.PlayerScores)
        {
            score.Buchholz = response.PlayerScores
                .Where(ps => score.PlayersFought.Contains(ps.PlayerId))
                .Sum(ps => ps.Score);
        }

        response.PlayerScores = response.PlayerScores
            .OrderByDescending(ps => ps.Score)
            .ThenByDescending(ps => ps.Buchholz)
            .ToList();

        for (var i = 0; i < response.PlayerScores.Count; i++)
        {
            response.PlayerScores[i].Position = i + 1;
        }
        
        return response;
    }
}