using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.DTOs;

public record CreatePredictionRequest(Guid MatchId, int HomeScore, int AwayScore);

public record UpdatePredictionRequest(int HomeScore, int AwayScore);

public record PredictionDto(
    Guid Id,
    Guid UserId,
    Guid MatchId,
    int HomeScore,
    int AwayScore,
    int? PointsEarned,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    MatchDto? Match);

public record MatchPredictionDto(
    Guid Id,
    string Username,
    int HomeScore,
    int AwayScore,
    int? PointsEarned);
