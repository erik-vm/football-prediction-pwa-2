using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IScoringService _scoringService;

    public MatchService(IMatchRepository matchRepository, IScoringService scoringService)
    {
        _matchRepository = matchRepository;
        _scoringService = scoringService;
    }

    public async Task<MatchDto> CreateAsync(CreateMatchRequest request)
    {
        var match = new Match
        {
            Id = Guid.NewGuid(),
            GameWeekId = request.GameWeekId,
            HomeTeam = request.HomeTeam,
            AwayTeam = request.AwayTeam,
            KickoffTime = request.KickoffTime,
            Stage = request.Stage
        };

        await _matchRepository.AddAsync(match);
        await _matchRepository.SaveChangesAsync();

        return ToDto(match);
    }

    public async Task<List<MatchDto>> GetByGameWeekIdAsync(Guid gameWeekId)
    {
        var matches = await _matchRepository.GetByGameWeekIdAsync(gameWeekId);
        return matches.Select(ToDto).ToList();
    }

    public async Task<List<MatchDto>> GetUpcomingAsync()
    {
        var matches = await _matchRepository.GetUpcomingAsync();
        return matches.Select(ToDto).ToList();
    }

    public async Task<MatchDto> GetByIdAsync(Guid id)
    {
        var match = await _matchRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Match not found.");
        return ToDto(match);
    }

    public async Task<MatchDto> UpdateAsync(Guid id, UpdateMatchRequest request)
    {
        var match = await _matchRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Match not found.");

        if (match.KickoffTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot edit match after kickoff.");

        match.HomeTeam = request.HomeTeam;
        match.AwayTeam = request.AwayTeam;
        match.KickoffTime = request.KickoffTime;
        match.Stage = request.Stage;

        _matchRepository.Update(match);
        await _matchRepository.SaveChangesAsync();

        return ToDto(match);
    }

    public async Task<MatchDto> EnterResultAsync(Guid id, EnterResultRequest request)
    {
        var match = await _matchRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Match not found.");

        match.HomeScore = request.HomeScore;
        match.AwayScore = request.AwayScore;
        match.IsFinished = true;

        _matchRepository.Update(match);
        await _matchRepository.SaveChangesAsync();

        await _scoringService.CalculatePointsForMatchAsync(id);

        return ToDto(match);
    }

    private static MatchDto ToDto(Match m) =>
        new(m.Id, m.GameWeekId, m.HomeTeam, m.AwayTeam, m.KickoffTime,
            m.Stage, m.StageMultiplier, m.HomeScore, m.AwayScore, m.IsFinished);
}
