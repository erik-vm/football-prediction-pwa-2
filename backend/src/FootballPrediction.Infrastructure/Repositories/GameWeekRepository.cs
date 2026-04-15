using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class GameWeekRepository : IGameWeekRepository
{
    private readonly AppDbContext _context;

    public GameWeekRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GameWeek?> GetByIdAsync(Guid id) =>
        await _context.GameWeeks.Include(g => g.Matches).FirstOrDefaultAsync(g => g.Id == id);

    public async Task<List<GameWeek>> GetByTournamentIdAsync(Guid tournamentId) =>
        await _context.GameWeeks
            .Where(g => g.TournamentId == tournamentId)
            .OrderBy(g => g.WeekNumber)
            .ToListAsync();

    public async Task AddAsync(GameWeek gameWeek) =>
        await _context.GameWeeks.AddAsync(gameWeek);

    public void Update(GameWeek gameWeek) =>
        _context.GameWeeks.Update(gameWeek);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
