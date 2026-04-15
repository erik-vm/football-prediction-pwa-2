using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/matches")]
[Authorize]
public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<List<MatchDto>>> GetUpcoming()
    {
        return Ok(await _matchService.GetUpcomingAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchDto>> GetById(Guid id)
    {
        return Ok(await _matchService.GetByIdAsync(id));
    }

    [HttpGet("gameweek/{gameWeekId:guid}")]
    public async Task<ActionResult<List<MatchDto>>> GetByGameWeek(Guid gameWeekId)
    {
        return Ok(await _matchService.GetByGameWeekIdAsync(gameWeekId));
    }
}
