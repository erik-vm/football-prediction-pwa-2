using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentService(ITournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
    }

    public async Task<TournamentDto> CreateAsync(CreateTournamentRequest request)
    {
        if (request.IsActive)
        {
            var active = await _tournamentRepository.GetActiveAsync();
            if (active != null)
                throw new InvalidOperationException("Another tournament is already active. Deactivate it first.");
        }

        var tournament = new Tournament
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Season = request.Season,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = request.IsActive
        };

        await _tournamentRepository.AddAsync(tournament);
        await _tournamentRepository.SaveChangesAsync();

        return ToDto(tournament);
    }

    public async Task<List<TournamentDto>> GetAllAsync()
    {
        var tournaments = await _tournamentRepository.GetAllAsync();
        return tournaments.Select(ToDto).ToList();
    }

    public async Task<TournamentDto?> GetActiveAsync()
    {
        var tournament = await _tournamentRepository.GetActiveAsync();
        return tournament != null ? ToDto(tournament) : null;
    }

    public async Task<TournamentDto> GetByIdAsync(Guid id)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Tournament not found.");
        return ToDto(tournament);
    }

    public async Task<TournamentDto> UpdateAsync(Guid id, UpdateTournamentRequest request)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Tournament not found.");

        if (request.IsActive && !tournament.IsActive)
        {
            var active = await _tournamentRepository.GetActiveAsync();
            if (active != null && active.Id != id)
                throw new InvalidOperationException("Another tournament is already active.");
        }

        tournament.Name = request.Name;
        tournament.Season = request.Season;
        tournament.StartDate = request.StartDate;
        tournament.EndDate = request.EndDate;
        tournament.IsActive = request.IsActive;

        _tournamentRepository.Update(tournament);
        await _tournamentRepository.SaveChangesAsync();

        return ToDto(tournament);
    }

    public async Task DeleteAsync(Guid id)
    {
        var tournament = await _tournamentRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Tournament not found.");

        _tournamentRepository.Delete(tournament);
        await _tournamentRepository.SaveChangesAsync();
    }

    private static TournamentDto ToDto(Tournament t) =>
        new(t.Id, t.Name, t.Season, t.StartDate, t.EndDate, t.IsActive);
}
