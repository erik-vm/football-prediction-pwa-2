using FootballPrediction.Application.Interfaces;

namespace FootballPrediction.Infrastructure.Services;

public class ScoringService : IScoringService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IPredictionRepository _predictionRepository;

    public ScoringService(IMatchRepository matchRepository, IPredictionRepository predictionRepository)
    {
        _matchRepository = matchRepository;
        _predictionRepository = predictionRepository;
    }

    public int CalculateBasePoints(int predictedHome, int predictedAway, int actualHome, int actualAway)
    {
        if (predictedHome == actualHome && predictedAway == actualAway)
            return 5;

        var predictedDiff = predictedHome - predictedAway;
        var actualDiff = actualHome - actualAway;

        if (HasSameWinner(predictedDiff, actualDiff) &&
            Math.Abs(predictedDiff) == Math.Abs(actualDiff))
            return 4;

        if (HasSameWinner(predictedDiff, actualDiff))
            return 3;

        if (predictedHome == actualHome || predictedAway == actualAway)
            return 1;

        return 0;
    }

    public async Task CalculatePointsForMatchAsync(Guid matchId)
    {
        var match = await _matchRepository.GetByIdWithPredictionsAsync(matchId);
        if (match == null || !match.IsFinished || !match.HomeScore.HasValue || !match.AwayScore.HasValue)
            return;

        var predictions = match.Predictions;
        if (predictions == null) return;

        foreach (var prediction in predictions)
        {
            var basePoints = CalculateBasePoints(
                prediction.HomeScore, prediction.AwayScore,
                match.HomeScore.Value, match.AwayScore.Value);

            prediction.PointsEarned = basePoints * match.StageMultiplier;
            _predictionRepository.Update(prediction);
        }

        await _predictionRepository.SaveChangesAsync();
    }

    private static bool HasSameWinner(int predictedDiff, int actualDiff)
    {
        if (predictedDiff == 0 && actualDiff == 0)
            return true;

        return (predictedDiff > 0 && actualDiff > 0) ||
               (predictedDiff < 0 && actualDiff < 0);
    }
}
