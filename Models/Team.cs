public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GameId { get; set; }

    public Game Game { get; set; } = null!;
    public ICollection<TeamPlayer> TeamPlayers { get; set; } = [];
}
