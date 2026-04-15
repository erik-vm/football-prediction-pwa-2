using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface ITournamentService
{
    Task<TournamentDto> CreateAsync(CreateTournamentRequest request);
    Task<List<TournamentDto>> GetAllAsync();
    Task<TournamentDto> GetByIdAsync(Guid id);
    Task<TournamentDto?> GetActiveAsync();
    Task<TournamentDto> UpdateAsync(Guid id, UpdateTournamentRequest request);
    Task DeleteAsync(Guid id);
}
