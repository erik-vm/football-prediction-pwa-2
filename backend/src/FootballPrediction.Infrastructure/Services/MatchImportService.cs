using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Domain.Entities;
using FootballPrediction.Domain.Enums;

namespace FootballPrediction.Infrastructure.Services;

public class MatchImportService : IMatchImportService
{
    private readonly IFootballDataService _footballDataService;
    private readonly IMatchRepository _matchRepository;
    private readonly IGameWeekRepository _gameWeekRepository;

    public MatchImportService(
        IFootballDataService footballDataService,
        IMatchRepository matchRepository,
        IGameWeekRepository gameWeekRepository)
    {
        _footballDataService = footballDataService;
        _matchRepository = matchRepository;
        _gameWeekRepository = gameWeekRepository;
    }

    public async Task<ImportMatchesResponse> ImportMatchesAsync(Guid gameWeekId, ImportMatchesRequest request)
    {
        var gameWeek = await _gameWeekRepository.GetByIdAsync(gameWeekId)
            ?? throw new KeyNotFoundException("Game week not found.");

        List<ExternalMatchDto> externalMatches;

        if (request.Matchday.HasValue && request.Season.HasValue)
        {
            externalMatches = await _footballDataService.GetMatchesAsync(
                request.Competition, request.Matchday.Value, request.Season.Value);
        }
        else if (request.DateFrom.HasValue && request.DateTo.HasValue)
        {
            externalMatches = await _footballDataService.GetMatchesByDateRangeAsync(
                request.Competition, request.DateFrom.Value, request.DateTo.Value);
        }
        else
        {
            throw new ArgumentException("Provide either matchday+season or dateFrom+dateTo.");
        }

        var existingMatches = await _matchRepository.GetByGameWeekIdAsync(gameWeekId);
        var existingKeys = existingMatches
            .Select(m => $"{m.HomeTeam}|{m.AwayTeam}|{m.KickoffTime:O}")
            .ToHashSet();

        int imported = 0, skipped = 0;
        var details = new List<string>();

        foreach (var ext in externalMatches)
        {
            var key = $"{ext.HomeTeam}|{ext.AwayTeam}|{ext.KickoffTime:O}";
            if (existingKeys.Contains(key))
            {
                skipped++;
                details.Add($"Skipped (duplicate): {ext.HomeTeam} vs {ext.AwayTeam}");
                continue;
            }

            var match = new Match
            {
                Id = Guid.NewGuid(),
                GameWeekId = gameWeekId,
                HomeTeam = ext.HomeTeam,
                AwayTeam = ext.AwayTeam,
                KickoffTime = ext.KickoffTime,
                Stage = MapStage(ext.Stage),
                HomeScore = ext.Status == "FINISHED" ? ext.HomeScore : null,
                AwayScore = ext.Status == "FINISHED" ? ext.AwayScore : null,
                IsFinished = ext.Status == "FINISHED"
            };

            await _matchRepository.AddAsync(match);
            imported++;
            details.Add($"Imported: {ext.HomeTeam} vs {ext.AwayTeam} ({ext.KickoffTime:g})");
        }

        if (imported > 0)
            await _matchRepository.SaveChangesAsync();

        return new ImportMatchesResponse(imported, skipped, details);
    }

    private static TournamentStage MapStage(string apiStage)
    {
        return apiStage.ToUpperInvariant() switch
        {
            "LEAGUE_STAGE" or "GROUP_STAGE" or "GROUP_STAGE_1" or "GROUP_STAGE_2" => TournamentStage.GROUP_STAGE,
            "LAST_16" or "ROUND_OF_16" => TournamentStage.ROUND_OF_16,
            "QUARTER_FINALS" => TournamentStage.QUARTER_FINALS,
            "SEMI_FINALS" => TournamentStage.SEMI_FINALS,
            "FINAL" => TournamentStage.FINAL,
            _ => TournamentStage.GROUP_STAGE
        };
    }
}
