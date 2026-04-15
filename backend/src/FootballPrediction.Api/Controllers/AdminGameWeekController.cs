using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Authorize(Roles = "ADMIN")]
public class AdminGameWeekController : ControllerBase
{
    private readonly IGameWeekService _gameWeekService;
    private readonly IMatchImportService _matchImportService;

    public AdminGameWeekController(IGameWeekService gameWeekService, IMatchImportService matchImportService)
    {
        _gameWeekService = gameWeekService;
        _matchImportService = matchImportService;
    }

    [HttpPost("api/v1/admin/gameweeks")]
    public async Task<ActionResult<GameWeekDto>> Create([FromBody] CreateGameWeekRequest request)
    {
        var result = await _gameWeekService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("api/v1/admin/tournaments/{tournamentId:guid}/gameweeks")]
    public async Task<ActionResult<List<GameWeekDto>>> GetByTournament(Guid tournamentId)
    {
        return Ok(await _gameWeekService.GetByTournamentIdAsync(tournamentId));
    }

    [HttpGet("api/v1/admin/gameweeks/{id:guid}")]
    public async Task<ActionResult<GameWeekDto>> GetById(Guid id)
    {
        return Ok(await _gameWeekService.GetByIdAsync(id));
    }

    [HttpPut("api/v1/admin/gameweeks/{id:guid}")]
    public async Task<ActionResult<GameWeekDto>> Update(Guid id, [FromBody] UpdateGameWeekRequest request)
    {
        return Ok(await _gameWeekService.UpdateAsync(id, request));
    }

    [HttpPost("api/v1/admin/gameweeks/{id:guid}/import-matches")]
    public async Task<ActionResult<ImportMatchesResponse>> ImportMatches(Guid id, [FromBody] ImportMatchesRequest request)
    {
        return Ok(await _matchImportService.ImportMatchesAsync(id, request));
    }
}
