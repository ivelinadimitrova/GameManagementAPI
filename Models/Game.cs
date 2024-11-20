public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;

    public ICollection<Leaderboard> Leaderboards { get; set; } = [];
    public ICollection<Achievement> Achievements { get; set; } = [];
    public ICollection<Team> Teams { get; set; } = [];
}
