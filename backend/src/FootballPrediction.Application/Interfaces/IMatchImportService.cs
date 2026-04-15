using FootballPrediction.Application.DTOs;

namespace FootballPrediction.Application.Interfaces;

public interface IMatchImportService
{
    Task<ImportMatchesResponse> ImportMatchesAsync(Guid gameWeekId, ImportMatchesRequest request);
}
