using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces;

public interface IMatchRepository
{
    Task<Match?> GetByIdAsync(Guid id);
    Task<Match?> GetByIdWithPredictionsAsync(Guid id);
    Task<List<Match>> GetByGameWeekIdAsync(Guid gameWeekId);
    Task<List<Match>> GetUpcomingAsync();
    Task AddAsync(Match match);
    Task AddRangeAsync(IEnumerable<Match> matches);
    void Update(Match match);
    Task SaveChangesAsync();
}
