using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Application.DTOs;

public record CreateMatchRequest(Guid GameWeekId, string HomeTeam, string AwayTeam, DateTime KickoffTime, TournamentStage Stage);

public record UpdateMatchRequest(string HomeTeam, string AwayTeam, DateTime KickoffTime, TournamentStage Stage);

public record EnterResultRequest(int HomeScore, int AwayScore);

public record MatchDto(
    Guid Id,
    Guid GameWeekId,
    string HomeTeam,
    string AwayTeam,
    DateTime KickoffTime,
    TournamentStage Stage,
    int StageMultiplier,
    int? HomeScore,
    int? AwayScore,
    bool IsFinished);
