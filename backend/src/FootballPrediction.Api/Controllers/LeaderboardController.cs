using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/leaderboard")]
[Authorize]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet("overall")]
    public async Task<ActionResult<List<LeaderboardEntryDto>>> GetOverall()
    {
        return Ok(await _leaderboardService.GetOverallLeaderboardAsync());
    }

    [HttpGet("weekly/{gameWeekId:guid}")]
    public async Task<ActionResult<List<WeeklyLeaderboardEntryDto>>> GetWeekly(Guid gameWeekId)
    {
        return Ok(await _leaderboardService.GetWeeklyLeaderboardAsync(gameWeekId));
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<UserStatsDto>> GetUserStats(Guid userId)
    {
        return Ok(await _leaderboardService.GetUserStatsAsync(userId));
    }
}
