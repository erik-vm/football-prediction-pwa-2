using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface IPredictionService
{
    Task<PredictionDto> CreateAsync(Guid userId, CreatePredictionRequest request);
    Task<PredictionDto> UpdateAsync(Guid userId, Guid predictionId, UpdatePredictionRequest request);
    Task<List<PredictionDto>> GetByUserAsync(Guid userId);
    Task<List<MatchPredictionDto>> GetByMatchAsync(Guid matchId);
}
