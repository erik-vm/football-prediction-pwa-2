using System.Security.Claims;
using FootballPrediction.Application.DTOs;
using FootballPrediction.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/predictions")]
[Authorize]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictionService;

    public PredictionController(IPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpPost]
    public async Task<ActionResult<PredictionDto>> Create([FromBody] CreatePredictionRequest request)
    {
        var userId = GetUserId();
        var result = await _predictionService.CreateAsync(userId, request);
        return CreatedAtAction(nameof(GetMy), result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PredictionDto>> Update(Guid id, [FromBody] UpdatePredictionRequest request)
    {
        var userId = GetUserId();
        return Ok(await _predictionService.UpdateAsync(userId, id, request));
    }

    [HttpGet("my")]
    public async Task<ActionResult<List<PredictionDto>>> GetMy()
    {
        var userId = GetUserId();
        return Ok(await _predictionService.GetByUserAsync(userId));
    }

    [HttpGet("match/{matchId:guid}")]
    public async Task<ActionResult<List<MatchPredictionDto>>> GetByMatch(Guid matchId)
    {
        return Ok(await _predictionService.GetByMatchAsync(matchId));
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim!.Value);
    }
}
