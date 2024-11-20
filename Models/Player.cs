public class Player
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    public ICollection<Leaderboard> Leaderboards { get; set; } = [];
    public ICollection<Achievement> Achievements { get; set; } = [];
    public ICollection<TeamPlayer> TeamPlayers { get; set; } = [];
}
