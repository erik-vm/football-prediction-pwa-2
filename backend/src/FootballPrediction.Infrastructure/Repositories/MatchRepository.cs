using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly AppDbContext _context;

    public MatchRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Match?> GetByIdAsync(Guid id) =>
        await _context.Matches.FindAsync(id);

    public async Task<Match?> GetByIdWithPredictionsAsync(Guid id) =>
        await _context.Matches
            .Include(m => m.Predictions!)
                .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<List<Match>> GetByGameWeekIdAsync(Guid gameWeekId) =>
        await _context.Matches
            .Where(m => m.GameWeekId == gameWeekId)
            .OrderBy(m => m.KickoffTime)
            .ToListAsync();

    public async Task<List<Match>> GetUpcomingAsync() =>
        await _context.Matches
            .Where(m => !m.IsFinished && m.KickoffTime > DateTime.UtcNow)
            .OrderBy(m => m.KickoffTime)
            .ToListAsync();

    public async Task AddAsync(Match match) =>
        await _context.Matches.AddAsync(match);

    public async Task AddRangeAsync(IEnumerable<Match> matches) =>
        await _context.Matches.AddRangeAsync(matches);

    public void Update(Match match) =>
        _context.Matches.Update(match);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
