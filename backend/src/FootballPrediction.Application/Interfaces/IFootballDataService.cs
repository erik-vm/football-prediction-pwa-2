using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface IFootballDataService
{
    Task<List<ExternalMatchDto>> GetMatchesAsync(string competition, int matchday, int season);
    Task<List<ExternalMatchDto>> GetMatchesByDateRangeAsync(string competition, DateTime from, DateTime to);
}
