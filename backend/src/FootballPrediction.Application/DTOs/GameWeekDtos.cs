namespace FootballPrediction.Application.DTOs;

public record CreateGameWeekRequest(Guid TournamentId, int WeekNumber, DateTime StartDate, DateTime EndDate);

public record UpdateGameWeekRequest(int WeekNumber, DateTime StartDate, DateTime EndDate);

public record GameWeekDto(Guid Id, Guid TournamentId, int WeekNumber, DateTime StartDate, DateTime EndDate);
