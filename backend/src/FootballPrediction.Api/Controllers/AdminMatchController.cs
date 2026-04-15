using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/admin/matches")]
[Authorize(Roles = "ADMIN")]
public class AdminMatchController : ControllerBase
{
    private readonly IMatchService _matchService;

    public AdminMatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpPost]
    public async Task<ActionResult<MatchDto>> Create([FromBody] CreateMatchRequest request)
    {
        var result = await _matchService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MatchDto>> GetById(Guid id)
    {
        return Ok(await _matchService.GetByIdAsync(id));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MatchDto>> Update(Guid id, [FromBody] UpdateMatchRequest request)
    {
        return Ok(await _matchService.UpdateAsync(id, request));
    }

    [HttpPut("{id:guid}/result")]
    public async Task<ActionResult<MatchDto>> EnterResult(Guid id, [FromBody] EnterResultRequest request)
    {
        return Ok(await _matchService.EnterResultAsync(id, request));
    }
}
