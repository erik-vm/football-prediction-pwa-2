using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    void Update(User user);
    Task SaveChangesAsync();
}
