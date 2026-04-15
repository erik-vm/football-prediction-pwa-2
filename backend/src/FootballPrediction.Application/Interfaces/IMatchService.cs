using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface IMatchService
{
    Task<MatchDto> CreateAsync(CreateMatchRequest request);
    Task<List<MatchDto>> GetByGameWeekIdAsync(Guid gameWeekId);
    Task<List<MatchDto>> GetUpcomingAsync();
    Task<MatchDto> GetByIdAsync(Guid id);
    Task<List<MatchWithPredictionDto>> GetByTournamentAsync(Guid tournamentId, Guid userId, string? stage = null);
    Task<MatchDto> UpdateAsync(Guid id, UpdateMatchRequest request);
    Task<MatchDto> EnterResultAsync(Guid id, EnterResultRequest request);
}
