namespace FootballPrediction.Application.DTOs;

public record CreateTournamentRequest(string Name, string Season, DateTime StartDate, DateTime EndDate, bool IsActive);

public record UpdateTournamentRequest(string Name, string Season, DateTime StartDate, DateTime EndDate, bool IsActive);

public record TournamentDto(Guid Id, string Name, string Season, DateTime StartDate, DateTime EndDate, bool IsActive);
