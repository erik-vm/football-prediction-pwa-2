using FootballPrediction.Domain.Enums;
using FootballPrediction.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/setup")]
public class SetupController : ControllerBase
{
    private readonly AppDbContext _context;

    public SetupController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("promote-admin/{email}")]
    public async Task<IActionResult> PromoteAdmin(string email)
    {
        var hasAdmin = await _context.Users.AnyAsync(u => u.Role == UserRole.ADMIN);
        if (hasAdmin)
            return BadRequest(new { error = "Admin already exists." });

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant());
        if (user == null)
            return NotFound(new { error = "User not found." });

        user.Role = UserRole.ADMIN;
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{user.Username} promoted to ADMIN." });
    }
}
