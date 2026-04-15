using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces;

public interface IGameWeekRepository
{
    Task<GameWeek?> GetByIdAsync(Guid id);
    Task<List<GameWeek>> GetByTournamentIdAsync(Guid tournamentId);
    Task AddAsync(GameWeek gameWeek);
    void Update(GameWeek gameWeek);
    Task SaveChangesAsync();
}
