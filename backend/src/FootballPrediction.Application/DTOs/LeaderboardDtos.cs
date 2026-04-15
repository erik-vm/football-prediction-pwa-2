namespace FootballPrediction.Application.DTOs;

public record LeaderboardEntryDto(
    int Rank,
    Guid UserId,
    string Username,
    int TotalPoints,
    int PredictionsMade,
    int ExactScores,
    int CorrectWinners);

public record WeeklyLeaderboardEntryDto(
    int Rank,
    Guid UserId,
    string Username,
    int WeeklyPoints,
    decimal WeeklyBonus);

public record UserStatsDto(
    Guid UserId,
    string Username,
    int OverallRank,
    int TotalPoints,
    int PredictionsMade,
    int TotalMatches,
    int ExactScores,
    int WinnerAndDiff,
    int CorrectWinners,
    int OneScore,
    int Misses);
