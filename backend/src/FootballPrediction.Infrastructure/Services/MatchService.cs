using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Infrastructure.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IPredictionRepository _predictionRepository;
    private readonly IScoringService _scoringService;

    public MatchService(IMatchRepository matchRepository, IPredictionRepository predictionRepository, IScoringService scoringService)
    {
        _matchRepository = matchRepository;
        _predictionRepository = predictionRepository;
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

    public async Task<List<MatchWithPredictionDto>> GetByTournamentAsync(Guid tournamentId, Guid userId, string? stage = null)
    {
        var matches = await _matchRepository.GetByTournamentIdAsync(tournamentId, stage);
        var matchIds = matches.Select(m => m.Id).ToList();
        var predictions = await _predictionRepository.GetByUserIdAsync(userId);
        var predictionMap = predictions
            .Where(p => matchIds.Contains(p.MatchId))
            .ToDictionary(p => p.MatchId);

        return matches.Select(m =>
        {
            predictionMap.TryGetValue(m.Id, out var pred);
            var userPred = pred != null
                ? new UserPredictionDto(pred.Id, pred.HomeScore, pred.AwayScore, pred.PointsEarned)
                : null;
            return new MatchWithPredictionDto(
                m.Id, m.GameWeekId, m.HomeTeam, m.AwayTeam, m.KickoffTime,
                m.Stage, m.StageMultiplier, m.HomeScore, m.AwayScore, m.IsFinished, userPred);
        }).ToList();
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
