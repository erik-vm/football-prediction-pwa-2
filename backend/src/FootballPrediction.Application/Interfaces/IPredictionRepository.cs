using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces;

public interface IPredictionRepository
{
    Task<Prediction?> GetByIdAsync(Guid id);
    Task<Prediction?> GetByUserAndMatchAsync(Guid userId, Guid matchId);
    Task<List<Prediction>> GetByUserIdAsync(Guid userId);
    Task<List<Prediction>> GetByMatchIdAsync(Guid matchId);
    Task<List<Prediction>> GetByUserAndGameWeekAsync(Guid userId, Guid gameWeekId);
    Task AddAsync(Prediction prediction);
    void Update(Prediction prediction);
    Task SaveChangesAsync();
}
