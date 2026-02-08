using SwissSystem.WebApp.DAL.Repositories.Interfaces;
using SwissSystem.WebApp.Models;
using SwissSystem.WebApp.Services.Interfaces;

namespace SwissSystem.WebApp.Services;

public class FinalsService(
    IFinalsBracketRepository bracketRepository
) : IFinalsService
{
    public async Task<FinalsBracket?> GetBracketAsync(int tournamentId)
    {
        return await bracketRepository.GetFromTournamentIdAsync(tournamentId);
    }

    public async Task<FinalsBracket> StartFinalsAsync(int tournamentId, IReadOnlyList<int> top4PlayerIdsInOrder)
    {
        if (top4PlayerIdsInOrder.Count != 4)
            throw new BadHttpRequestException("must have 4 semifinalists");

        var newBracket = new FinalsBracket()
        {
            TournamentId = tournamentId,
            SemifinalsAPlayer1Id = top4PlayerIdsInOrder[0],
            SemifinalsAPlayer2Id = top4PlayerIdsInOrder[3],
            SemifinalsBPlayer1Id = top4PlayerIdsInOrder[1],
            SemifinalsBPlayer2Id = top4PlayerIdsInOrder[2],
        };
        
        return await bracketRepository.InsertAsync(newBracket);
    }

    public async Task<FinalsBracket> SetSemifinalAWinnerAsync(int tournamentId, int winnerPlayerId)
    {
        var bracket = await bracketRepository.GetFromTournamentIdAsync(tournamentId);
        if (bracket is null)
            throw new BadHttpRequestException("bracket not found");
        
        bracket.FinalsPlayer1Id = winnerPlayerId;
        return await bracketRepository.UpdateAsync(bracket);
    }

    public async Task<FinalsBracket> SetSemifinalBWinnerAsync(int tournamentId, int winnerPlayerId)
    {
        var bracket = await bracketRepository.GetFromTournamentIdAsync(tournamentId);
        if (bracket is null)
            throw new BadHttpRequestException("bracket not found");
        
        bracket.FinalsPlayer2Id = winnerPlayerId;
        return await bracketRepository.UpdateAsync(bracket);
    }

    public async Task<FinalsBracket> SetChampionAsync(int tournamentId, int championPlayerId)
    {
        var bracket = await bracketRepository.GetFromTournamentIdAsync(tournamentId);
        if (bracket is null)
            throw new BadHttpRequestException("bracket not found");
        
        bracket.WinnerId = championPlayerId;
        return await bracketRepository.UpdateAsync(bracket);
    }
}