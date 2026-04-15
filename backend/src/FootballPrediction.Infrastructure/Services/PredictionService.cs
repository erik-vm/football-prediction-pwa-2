using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Infrastructure.Services;

public class PredictionService : IPredictionService
{
    private readonly IPredictionRepository _predictionRepository;
    private readonly IMatchRepository _matchRepository;

    public PredictionService(IPredictionRepository predictionRepository, IMatchRepository matchRepository)
    {
        _predictionRepository = predictionRepository;
        _matchRepository = matchRepository;
    }

    public async Task<PredictionDto> CreateAsync(Guid userId, CreatePredictionRequest request)
    {
        var match = await _matchRepository.GetByIdAsync(request.MatchId)
            ?? throw new KeyNotFoundException("Match not found.");

        if (match.KickoffTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot submit prediction after kickoff.");

        var existing = await _predictionRepository.GetByUserAndMatchAsync(userId, request.MatchId);
        if (existing != null)
            throw new InvalidOperationException("Prediction already exists. Use update instead.");

        ValidateScores(request.HomeScore, request.AwayScore);

        var prediction = new Prediction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MatchId = request.MatchId,
            HomeScore = request.HomeScore,
            AwayScore = request.AwayScore
        };

        await _predictionRepository.AddAsync(prediction);
        await _predictionRepository.SaveChangesAsync();

        return ToDto(prediction, ToMatchDto(match));
    }

    public async Task<PredictionDto> UpdateAsync(Guid userId, Guid predictionId, UpdatePredictionRequest request)
    {
        var prediction = await _predictionRepository.GetByIdAsync(predictionId)
            ?? throw new KeyNotFoundException("Prediction not found.");

        if (prediction.UserId != userId)
            throw new UnauthorizedAccessException("Cannot update another user's prediction.");

        if (prediction.Match!.KickoffTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Cannot update prediction after kickoff.");

        ValidateScores(request.HomeScore, request.AwayScore);

        prediction.HomeScore = request.HomeScore;
        prediction.AwayScore = request.AwayScore;

        _predictionRepository.Update(prediction);
        await _predictionRepository.SaveChangesAsync();

        return ToDto(prediction, ToMatchDto(prediction.Match));
    }

    public async Task<List<PredictionDto>> GetByUserAsync(Guid userId)
    {
        var predictions = await _predictionRepository.GetByUserIdAsync(userId);
        return predictions.Select(p => ToDto(p, p.Match != null ? ToMatchDto(p.Match) : null)).ToList();
    }

    public async Task<List<MatchPredictionDto>> GetByMatchAsync(Guid matchId)
    {
        var match = await _matchRepository.GetByIdAsync(matchId)
            ?? throw new KeyNotFoundException("Match not found.");

        if (!match.IsFinished)
            throw new InvalidOperationException("Predictions are hidden until the match is finished.");

        var predictions = await _predictionRepository.GetByMatchIdAsync(matchId);
        return predictions.Select(p => new MatchPredictionDto(
            p.Id, p.User?.Username ?? "", p.HomeScore, p.AwayScore, p.PointsEarned)).ToList();
    }

    private static void ValidateScores(int homeScore, int awayScore)
    {
        if (homeScore < 0 || homeScore > 9)
            throw new ArgumentException("Home score must be between 0 and 9.");
        if (awayScore < 0 || awayScore > 9)
            throw new ArgumentException("Away score must be between 0 and 9.");
    }

    private static PredictionDto ToDto(Prediction p, MatchDto? matchDto) =>
        new(p.Id, p.UserId, p.MatchId, p.HomeScore, p.AwayScore,
            p.PointsEarned, p.CreatedAt, p.UpdatedAt, matchDto);

    private static MatchDto ToMatchDto(Match m) =>
        new(m.Id, m.GameWeekId, m.HomeTeam, m.AwayTeam, m.KickoffTime,
            m.Stage, m.StageMultiplier, m.HomeScore, m.AwayScore, m.IsFinished);
}
