public class Achievement
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PlayerId { get; set; }
    public int GameId { get; set; }

    public Player Player { get; set; } = null!;
    public Game Game { get; set; } = null!;
}
