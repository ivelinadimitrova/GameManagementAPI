public class TeamPlayer
{
    public int TeamId { get; set; }
    public int PlayerId { get; set; }

    public Team Team { get; set; } = null!;
    public Player Player { get; set; } = null!;
}
