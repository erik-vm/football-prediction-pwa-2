namespace FootballPrediction.Application.Interfaces;

public interface IScoringService
{
    int CalculateBasePoints(int predictedHome, int predictedAway, int actualHome, int actualAway);
    Task CalculatePointsForMatchAsync(Guid matchId);
}
