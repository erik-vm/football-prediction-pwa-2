using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface ILeaderboardService
{
    Task<List<LeaderboardEntryDto>> GetOverallLeaderboardAsync();
    Task<List<WeeklyLeaderboardEntryDto>> GetWeeklyLeaderboardAsync(Guid gameWeekId);
    Task<UserStatsDto> GetUserStatsAsync(Guid userId);
}
