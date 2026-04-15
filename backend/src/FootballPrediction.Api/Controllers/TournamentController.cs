using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/tournaments")]
[Authorize]
public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;

    public TournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet("active")]
    public async Task<ActionResult<TournamentDto>> GetActive()
    {
        var tournament = await _tournamentService.GetActiveAsync();
        if (tournament == null)
            return NotFound(new { error = "No active tournament." });
        return Ok(tournament);
    }
}
