using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class GameWeekService : IGameWeekService
{
    private readonly IGameWeekRepository _gameWeekRepository;
    private readonly ITournamentRepository _tournamentRepository;

    public GameWeekService(IGameWeekRepository gameWeekRepository, ITournamentRepository tournamentRepository)
    {
        _gameWeekRepository = gameWeekRepository;
        _tournamentRepository = tournamentRepository;
    }

    public async Task<GameWeekDto> CreateAsync(CreateGameWeekRequest request)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(request.TournamentId)
            ?? throw new KeyNotFoundException("Tournament not found.");

        var gameWeek = new GameWeek
        {
            Id = Guid.NewGuid(),
            TournamentId = request.TournamentId,
            WeekNumber = request.WeekNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await _gameWeekRepository.AddAsync(gameWeek);
        await _gameWeekRepository.SaveChangesAsync();

        return ToDto(gameWeek);
    }

    public async Task<List<GameWeekDto>> GetByTournamentIdAsync(Guid tournamentId)
    {
        var gameWeeks = await _gameWeekRepository.GetByTournamentIdAsync(tournamentId);
        return gameWeeks.Select(ToDto).ToList();
    }

    public async Task<GameWeekDto> GetByIdAsync(Guid id)
    {
        var gameWeek = await _gameWeekRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Game week not found.");
        return ToDto(gameWeek);
    }

    public async Task<GameWeekDto> UpdateAsync(Guid id, UpdateGameWeekRequest request)
    {
        var gameWeek = await _gameWeekRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Game week not found.");

        gameWeek.WeekNumber = request.WeekNumber;
        gameWeek.StartDate = request.StartDate;
        gameWeek.EndDate = request.EndDate;

        _gameWeekRepository.Update(gameWeek);
        await _gameWeekRepository.SaveChangesAsync();

        return ToDto(gameWeek);
    }

    private static GameWeekDto ToDto(GameWeek g) =>
        new(g.Id, g.TournamentId, g.WeekNumber, g.StartDate, g.EndDate);
}
