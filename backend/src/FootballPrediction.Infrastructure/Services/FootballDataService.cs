using System.Net.Http.Json;
using System.Text.Json;
using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FootballPrediction.Infrastructure.Services;

public class FootballDataService : IFootballDataService
{
    private readonly HttpClient _httpClient;

    public FootballDataService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["FootballDataApi:BaseUrl"]!);
        _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", configuration["FootballDataApi:ApiKey"]);
    }

    public async Task<List<ExternalMatchDto>> GetMatchesAsync(string competition, int matchday, int season)
    {
        var url = $"/v4/competitions/{competition}/matches?matchday={matchday}&season={season}";
        return await FetchAndParseAsync(url);
    }

    public async Task<List<ExternalMatchDto>> GetMatchesByDateRangeAsync(string competition, DateTime from, DateTime to)
    {
        var url = $"/v4/competitions/{competition}/matches?dateFrom={from:yyyy-MM-dd}&dateTo={to:yyyy-MM-dd}";
        return await FetchAndParseAsync(url);
    }

    private async Task<List<ExternalMatchDto>> FetchAndParseAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var matches = new List<ExternalMatchDto>();

        if (!json.TryGetProperty("matches", out var matchesArray))
            return matches;

        foreach (var m in matchesArray.EnumerateArray())
        {
            var homeTeam = m.GetProperty("homeTeam").GetProperty("shortName").GetString() ?? "Unknown";
            var awayTeam = m.GetProperty("awayTeam").GetProperty("shortName").GetString() ?? "Unknown";
            var utcDate = m.GetProperty("utcDate").GetDateTime();
            var stage = m.GetProperty("stage").GetString() ?? "LEAGUE_STAGE";
            var status = m.GetProperty("status").GetString() ?? "SCHEDULED";
            var externalId = m.GetProperty("id").GetInt32();

            int? homeScore = null;
            int? awayScore = null;

            if (m.TryGetProperty("score", out var score) &&
                score.TryGetProperty("fullTime", out var fullTime))
            {
                if (fullTime.TryGetProperty("home", out var h) && h.ValueKind == JsonValueKind.Number)
                    homeScore = h.GetInt32();
                if (fullTime.TryGetProperty("away", out var a) && a.ValueKind == JsonValueKind.Number)
                    awayScore = a.GetInt32();
            }

            matches.Add(new ExternalMatchDto(externalId, homeTeam, awayTeam, utcDate, stage, status, homeScore, awayScore));
        }

        return matches;
    }
}
