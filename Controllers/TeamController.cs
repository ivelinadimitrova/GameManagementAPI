using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly GameManagementContext _context;

    public TeamController(GameManagementContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        return await _context.Teams.Include(t => t.Game).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await _context.Teams
            .Include(t => t.Game)
            .Include(t => t.TeamPlayers)
            .ThenInclude(tp => tp.Player)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team == null) return NotFound();
        return team;
    }

    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam(int id, Team team)
    {
        if (id != team.Id) return BadRequest();

        _context.Entry(team).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Teams.Any(t => t.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return NotFound();

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
