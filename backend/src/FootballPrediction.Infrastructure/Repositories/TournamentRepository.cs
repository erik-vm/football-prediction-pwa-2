using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Infrastructure.Repositories;

public class TournamentRepository : ITournamentRepository
{
    private readonly AppDbContext _context;

    public TournamentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Tournament?> GetByIdAsync(Guid id) =>
        await _context.Tournaments.Include(t => t.GameWeeks).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<List<Tournament>> GetAllAsync() =>
        await _context.Tournaments.OrderByDescending(t => t.StartDate).ToListAsync();

    public async Task<Tournament?> GetActiveAsync() =>
        await _context.Tournaments.FirstOrDefaultAsync(t => t.IsActive);

    public async Task AddAsync(Tournament tournament) =>
        await _context.Tournaments.AddAsync(tournament);

    public void Update(Tournament tournament) =>
        _context.Tournaments.Update(tournament);

    public void Delete(Tournament tournament) =>
        _context.Tournaments.Remove(tournament);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
