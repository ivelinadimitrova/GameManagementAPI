public class Leaderboard
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int Score { get; set; }

    public Game Game { get; set; } = null!;
    public Player Player { get; set; } = null!;
}
