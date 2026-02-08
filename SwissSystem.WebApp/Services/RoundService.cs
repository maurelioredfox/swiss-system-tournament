using System.Collections;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;
using SwissSystem.WebApp.Services.Interfaces;

namespace SwissSystem.WebApp.Services;

public class RoundService(
    ITournamentRepository tournamentRepository,
    IRoundRepository roundRepository,
    IScoresService scoresService,
    IMatchRepository matchRepository
) : IRoundService
{
    public async Task<IReadOnlyList<RoundView>> GetRoundsAsync(int tournamentId)
    {
        var fromDb = await roundRepository.GetByTournamentIdAsync(tournamentId);
        var roundViews = new  List<RoundView>();

        foreach (var round in fromDb)
        {
            roundViews.Add(await RoundToRoundViewAsync(round));
        }
        
        return roundViews;
    }

    public async Task<RoundView?> GetRoundAsync(int tournamentId, int roundId)
    {
        var fromDb = await roundRepository.GetByIdAsync(roundId);
        
        if (fromDb == null)
            return null;
        
        return await RoundToRoundViewAsync(fromDb);
    }

    public async Task<RoundView> GenerateNextRoundAsync(int tournamentId)
    {
        var tournament = await tournamentRepository.GetByIdAsync(tournamentId);
        if  (tournament == null) throw new BadHttpRequestException("No tournament found");
        
        var newRound = new Round()
        {
            Number = tournament.Rounds.Count + 1,
            TournamentId = tournamentId
        };
        
        var scoresBoard = await scoresService.GetScoresAsync(tournamentId);
        var playerBuckets = scoresBoard.PlayerScores.GroupBy(ps => ps.Score)
            .ToDictionary(k => k.Key, v => v.ToList());
        var matches = new List<Match>();

        if (scoresBoard.PlayerScores.Count % 2 == 1)
        {
            //get the bye player
            PlayerScoreView[] candidates = [];
            foreach (var bucketKey in playerBuckets.Keys.Order())
            {
                candidates = playerBuckets[bucketKey].Where(ps => !ps.hasBye).ToArray();
                if (candidates.Length > 0) break;
            }
            
            var byePlayer = Random.Shared.GetItems<PlayerScoreView>(candidates, 1).First();
            playerBuckets[byePlayer.Score].Remove(byePlayer);

            var match = new Match()
            {
                Player1Id = byePlayer.PlayerId,
                Bye = true,
            };
            matches.Add(match);
        }

        foreach (var keyValuePair in playerBuckets)
        {
            playerBuckets[keyValuePair.Key] = keyValuePair.Value.Shuffle().ToList();
        }

        var collapsedList = playerBuckets.Values.SelectMany(i => i).ToList();
        List<Tuple<PlayerScoreView, PlayerScoreView>> matchPairs;
        
        try
        {
            matchPairs = TryToPairPlayers(collapsedList);
        }
        catch (PairingException)
        {
            matchPairs = TryToPairPlayers(collapsedList, true);
        }

        foreach (var matchPair in matchPairs)
        {
            matches.Add(new Match()
            {
                Bye = false,
                Player1Id = matchPair.Item1.PlayerId,
                Player2Id = matchPair.Item2.PlayerId,
            });
        }
        
        newRound.Matches = matches;
        var response = await roundRepository.AddAsync(newRound);
        
        return await RoundToRoundViewAsync(response);
    }
    
    /// <summary>
    /// Recursively creates a pair and calls itself until the list is emptied, tries different pairs if it gets stuck
    /// </summary>
    /// <param name="playerScores"></param>
    /// <returns></returns>
    private static List<Tuple<PlayerScoreView, PlayerScoreView>> TryToPairPlayers(List<PlayerScoreView> playerScores, bool allowRematch = false)
    {
        var p1 = playerScores.First();

        var candidates = playerScores.Skip(1)
            .Where(p => !p.PlayersFought.Contains(p1.PlayerId)).ToList();

        if (candidates.Count == 0)
        {
            candidates = playerScores.Skip(1).ToList();
        }

        foreach (var p2 in candidates)
        {
            var remaining = new List<PlayerScoreView>(playerScores);
            remaining.Remove(p1);
            remaining.Remove(p2);
            try
            {
                if (remaining.Count == 0) return [Tuple.Create(p1, p2)];
                var result = TryToPairPlayers(remaining, allowRematch);
                result.Add(Tuple.Create(p1, p2));
                return result;
            }
            catch (PairingException)
            {
                // try next p2
            }
        }

        throw new PairingException();
    }
    
    private class PairingException : Exception
    {
        
    }

    public async Task SetMatchResultAsync(int tournamentId, int matchId, Result result)
    {
        var match = await matchRepository.GetByIdAsync(matchId);
        match!.WinnerId = result switch
        {
            Result.Player1Wins => match.Player1Id,
            Result.Player2Wins => match.Player2Id,
            _ => null
        };
        await matchRepository.UpdateAsync(match);
    }

    public async Task DeleteLastRoundAsync(int tournamentId)
    {
        var round = (await roundRepository.GetByTournamentIdAsync(tournamentId))
            .OrderBy(r => r.Number).Last();
        await roundRepository.RemoveAsync(round);
    }
    
    private async Task<RoundView> RoundToRoundViewAsync(Round round)
    {
        var matches = new List<MatchView>();
        
        foreach (var match in round.Matches)
        {
            var matchView = new MatchView()
            {
                Id = match.Id,
                Player1Name = match.Player1.Name,
                Player2Name = match.Player2?.Name ?? "BYE",
                Player1Score = await GetOutstandingScoreAsync(round.TournamentId, match.Player1Id),
                Player2Score = match.Player2Id.HasValue ? 
                    await GetOutstandingScoreAsync(round.TournamentId, match.Player2Id.Value) : 0,
                Result = match.Bye? Result.Bye : !match.WinnerId.HasValue ? Result.Pending :
                    match.WinnerId == match.Player1Id ? Result.Player1Wins : Result.Player2Wins
            };
                
            matches.Add(matchView);
        }
        
        var roundView = new RoundView()
        {
            Id = round.Id,
            Matches = matches
        };
        
        return roundView;
    }

    private async Task<int> GetOutstandingScoreAsync(int tournamentId, int playerId)
    {
        var rounds = await roundRepository.GetByTournamentIdAsync(tournamentId);
        
        if (rounds == null)
            throw new BadHttpRequestException("No tournament found");
        
        var playerMatches = rounds
            .SelectMany(r => r.Matches)
            .Where(m => m.Player1Id == playerId || m.Player2Id == playerId)
            .ToList();

        var wins = playerMatches.Count(m => m.WinnerId == playerId);
        var byes = playerMatches.Count(m => m.Bye);
        return (byes + wins) * 3;
    }
}