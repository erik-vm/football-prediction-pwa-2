using FootballPrediction.Application.Interfaces;
using FootballPrediction.Infrastructure.Repositories;
using FootballPrediction.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPrediction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        services.AddScoped<IGameWeekRepository, GameWeekRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IPredictionRepository, PredictionRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<IGameWeekService, GameWeekService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<IScoringService, ScoringService>();
        services.AddScoped<IPredictionService, PredictionService>();
        services.AddScoped<ILeaderboardService, LeaderboardService>();

        services.AddHttpClient<IFootballDataService, FootballDataService>();
        services.AddScoped<IMatchImportService, MatchImportService>();

        return services;
    }
}
