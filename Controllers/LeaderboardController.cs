using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly GameManagementContext _context;

    public LeaderboardController(GameManagementContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Leaderboard>>> GetLeaderboards()
    {
        return await _context.Leaderboards
            .Include(l => l.Game)
            .Include(l => l.Player)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Leaderboard>> GetLeaderboard(int id)
    {
        var leaderboard = await _context.Leaderboards
            .Include(l => l.Game)
            .Include(l => l.Player)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (leaderboard == null) return NotFound();
        return leaderboard;
    }

    [HttpPost]
    public async Task<ActionResult<Leaderboard>> CreateLeaderboard(Leaderboard leaderboard)
    {
        _context.Leaderboards.Add(leaderboard);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLeaderboard), new { id = leaderboard.Id }, leaderboard);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLeaderboard(int id, Leaderboard leaderboard)
    {
        if (id != leaderboard.Id) return BadRequest();

        _context.Entry(leaderboard).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Leaderboards.Any(l => l.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLeaderboard(int id)
    {
        var leaderboard = await _context.Leaderboards.FindAsync(id);
        if (leaderboard == null) return NotFound();

        _context.Leaderboards.Remove(leaderboard);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
