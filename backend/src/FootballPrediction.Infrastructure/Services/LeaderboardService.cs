using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Enums;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly AppDbContext _context;

    public LeaderboardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LeaderboardEntryDto>> GetOverallLeaderboardAsync()
    {
        var entries = await _context.Predictions
            .Where(p => p.PointsEarned.HasValue)
            .GroupBy(p => new { p.UserId, p.User!.Username })
            .Select(g => new
            {
                g.Key.UserId,
                g.Key.Username,
                TotalPoints = g.Sum(p => p.PointsEarned!.Value),
                PredictionsMade = g.Count(),
                ExactScores = g.Count(p => p.PointsEarned == 5 * p.Match!.Stage.GetMultiplier()),
                CorrectWinners = g.Count(p => p.PointsEarned >= 3 * p.Match!.Stage.GetMultiplier())
            })
            .OrderByDescending(x => x.TotalPoints)
            .ThenByDescending(x => x.ExactScores)
            .ThenByDescending(x => x.CorrectWinners)
            .ThenBy(x => x.Username)
            .ToListAsync();

        return entries.Select((e, i) => new LeaderboardEntryDto(
            i + 1, e.UserId, e.Username, e.TotalPoints,
            e.PredictionsMade, e.ExactScores, e.CorrectWinners)).ToList();
    }

    public async Task<List<WeeklyLeaderboardEntryDto>> GetWeeklyLeaderboardAsync(Guid gameWeekId)
    {
        var entries = await _context.Predictions
            .Where(p => p.Match!.GameWeekId == gameWeekId && p.PointsEarned.HasValue)
            .GroupBy(p => new { p.UserId, p.User!.Username })
            .Select(g => new
            {
                g.Key.UserId,
                g.Key.Username,
                WeeklyPoints = g.Sum(p => p.PointsEarned!.Value)
            })
            .OrderByDescending(x => x.WeeklyPoints)
            .ThenBy(x => x.Username)
            .ToListAsync();

        var bonuses = CalculateWeeklyBonuses(entries.Select(e => new UserWeeklyScore(e.UserId, e.WeeklyPoints)).ToList());

        return entries.Select((e, i) => new WeeklyLeaderboardEntryDto(
            i + 1, e.UserId, e.Username, e.WeeklyPoints,
            bonuses.GetValueOrDefault(e.UserId, 0m))).ToList();
    }

    public async Task<UserStatsDto> GetUserStatsAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId)
            ?? throw new KeyNotFoundException("User not found.");

        var predictions = await _context.Predictions
            .Include(p => p.Match)
            .Where(p => p.UserId == userId && p.PointsEarned.HasValue)
            .ToListAsync();

        var totalMatches = await _context.Matches.CountAsync(m => m.IsFinished);
        var predictionsMade = predictions.Count;

        int exactScores = 0, winnerAndDiff = 0, correctWinners = 0, oneScore = 0, misses = 0;

        foreach (var p in predictions)
        {
            var basePoints = p.Match!.StageMultiplier > 0
                ? p.PointsEarned!.Value / p.Match.StageMultiplier
                : 0;

            switch (basePoints)
            {
                case 5: exactScores++; break;
                case 4: winnerAndDiff++; break;
                case 3: correctWinners++; break;
                case 1: oneScore++; break;
                default: misses++; break;
            }
        }

        var leaderboard = await GetOverallLeaderboardAsync();
        var rank = leaderboard.FindIndex(e => e.UserId == userId) + 1;
        var totalPoints = leaderboard.FirstOrDefault(e => e.UserId == userId)?.TotalPoints ?? 0;

        return new UserStatsDto(userId, user.Username, rank == 0 ? predictionsMade > 0 ? leaderboard.Count + 1 : 0 : rank,
            totalPoints, predictionsMade, totalMatches,
            exactScores, winnerAndDiff, correctWinners, oneScore, misses);
    }

    private static Dictionary<Guid, decimal> CalculateWeeklyBonuses(List<UserWeeklyScore> scores)
    {
        var bonuses = new Dictionary<Guid, decimal>();
        if (scores.Count == 0) return bonuses;

        var bonusValues = new[] { 5m, 3m, 1m };
        var sortedScores = scores.OrderByDescending(s => s.Points).ToList();

        int position = 0;
        int i = 0;

        while (i < sortedScores.Count && position < 3)
        {
            var currentPoints = sortedScores[i].Points;
            var tiedUsers = sortedScores.Where(s => s.Points == currentPoints).ToList();

            var totalBonus = 0m;
            for (int b = position; b < Math.Min(position + tiedUsers.Count, 3); b++)
                totalBonus += bonusValues[b];

            var bonusPerUser = totalBonus / tiedUsers.Count;

            foreach (var user in tiedUsers)
                bonuses[user.UserId] = bonusPerUser;

            position += tiedUsers.Count;
            i += tiedUsers.Count;
        }

        return bonuses;
    }

    private record UserWeeklyScore(Guid UserId, int Points);
}
