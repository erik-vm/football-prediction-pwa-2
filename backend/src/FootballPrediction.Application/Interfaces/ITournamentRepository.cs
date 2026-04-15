using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces;

public interface ITournamentRepository
{
    Task<Tournament?> GetByIdAsync(Guid id);
    Task<List<Tournament>> GetAllAsync();
    Task<Tournament?> GetActiveAsync();
    Task AddAsync(Tournament tournament);
    void Update(Tournament tournament);
    void Delete(Tournament tournament);
    Task SaveChangesAsync();
}
