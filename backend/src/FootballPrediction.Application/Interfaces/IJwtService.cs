using FootballPrediction.Domain.Entities;

namespace FootballPrediction.Application.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
