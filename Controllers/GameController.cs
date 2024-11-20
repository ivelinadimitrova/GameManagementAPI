using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameManagementContext _context;

    public GameController(GameManagementContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Game>>> GetGames()
    {
        return await _context.Games.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetGame(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return NotFound();
        return game;
    }

    [HttpPost]
    public async Task<ActionResult<Game>> CreateGame(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(int id, Game game)
    {
        if (id != game.Id) return BadRequest();
        _context.Entry(game).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return NotFound();
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
