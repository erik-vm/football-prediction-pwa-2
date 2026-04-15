using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface IGameWeekService
{
    Task<GameWeekDto> CreateAsync(CreateGameWeekRequest request);
    Task<List<GameWeekDto>> GetByTournamentIdAsync(Guid tournamentId);
    Task<GameWeekDto> GetByIdAsync(Guid id);
    Task<GameWeekDto> UpdateAsync(Guid id, UpdateGameWeekRequest request);
}
