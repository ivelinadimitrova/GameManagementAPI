using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AchievementController : ControllerBase
{
    private readonly GameManagementContext _context;

    public AchievementController(GameManagementContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievements()
    {
        return await _context.Achievements
            .Include(a => a.Game)
            .Include(a => a.Player)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Achievement>> GetAchievement(int id)
    {
        var achievement = await _context.Achievements
            .Include(a => a.Game)
            .Include(a => a.Player)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (achievement == null) return NotFound();
        return achievement;
    }

    [HttpPost]
    public async Task<ActionResult<Achievement>> CreateAchievement(Achievement achievement)
    {
        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAchievement), new { id = achievement.Id }, achievement);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAchievement(int id, Achievement achievement)
    {
        if (id != achievement.Id) return BadRequest();

        _context.Entry(achievement).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Achievements.Any(a => a.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAchievement(int id)
    {
        var achievement = await _context.Achievements.FindAsync(id);
        if (achievement == null) return NotFound();

        _context.Achievements.Remove(achievement);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
