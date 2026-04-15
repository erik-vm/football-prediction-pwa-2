using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/admin/tournaments")]
[Authorize(Roles = "ADMIN")]
public class AdminTournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;

    public AdminTournamentController(ITournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpPost]
    public async Task<ActionResult<TournamentDto>> Create([FromBody] CreateTournamentRequest request)
    {
        var result = await _tournamentService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<TournamentDto>>> GetAll()
    {
        return Ok(await _tournamentService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TournamentDto>> GetById(Guid id)
    {
        return Ok(await _tournamentService.GetByIdAsync(id));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TournamentDto>> Update(Guid id, [FromBody] UpdateTournamentRequest request)
    {
        return Ok(await _tournamentService.UpdateAsync(id, request));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _tournamentService.DeleteAsync(id);
        return NoContent();
    }
}
