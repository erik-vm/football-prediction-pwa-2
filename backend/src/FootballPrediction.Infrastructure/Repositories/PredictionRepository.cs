using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class PredictionRepository : IPredictionRepository
{
    private readonly AppDbContext _context;

    public PredictionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Prediction?> GetByIdAsync(Guid id) =>
        await _context.Predictions.Include(p => p.Match).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Prediction?> GetByUserAndMatchAsync(Guid userId, Guid matchId) =>
        await _context.Predictions.FirstOrDefaultAsync(p => p.UserId == userId && p.MatchId == matchId);

    public async Task<List<Prediction>> GetByUserIdAsync(Guid userId) =>
        await _context.Predictions
            .Include(p => p.Match)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.Match!.KickoffTime)
            .ToListAsync();

    public async Task<List<Prediction>> GetByMatchIdAsync(Guid matchId) =>
        await _context.Predictions
            .Include(p => p.User)
            .Where(p => p.MatchId == matchId)
            .ToListAsync();

    public async Task<List<Prediction>> GetByUserAndGameWeekAsync(Guid userId, Guid gameWeekId) =>
        await _context.Predictions
            .Include(p => p.Match)
            .Where(p => p.UserId == userId && p.Match!.GameWeekId == gameWeekId)
            .ToListAsync();

    public async Task AddAsync(Prediction prediction) =>
        await _context.Predictions.AddAsync(prediction);

    public void Update(Prediction prediction) =>
        _context.Predictions.Update(prediction);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
