namespace FootballPrediction.Application.DTOs;

public record ExternalMatchDto(
    int ExternalId,
    string HomeTeam,
    string AwayTeam,
    DateTime KickoffTime,
    string Stage,
    string Status,
    int? HomeScore,
    int? AwayScore);

public record ImportMatchesRequest(
    string Competition,
    int? Matchday,
    int? Season,
    DateTime? DateFrom,
    DateTime? DateTo);

public record ImportMatchesResponse(int Imported, int Skipped, List<string> Details);
