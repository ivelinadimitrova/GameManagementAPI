using Microsoft.EntityFrameworkCore;

public class GameManagementContext : DbContext
{
    public GameManagementContext(DbContextOptions<GameManagementContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Leaderboard> Leaderboards { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamPlayer> TeamPlayers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Leaderboard relationships
        modelBuilder.Entity<Leaderboard>()
            .HasOne(l => l.Game)
            .WithMany(g => g.Leaderboards)
            .HasForeignKey(l => l.GameId);

        modelBuilder.Entity<Leaderboard>()
            .HasOne(l => l.Player)
            .WithMany(p => p.Leaderboards)
            .HasForeignKey(l => l.PlayerId);

        // Configure Achievement relationships
        modelBuilder.Entity<Achievement>()
            .HasOne(a => a.Game)
            .WithMany(g => g.Achievements)
            .HasForeignKey(a => a.GameId);

        modelBuilder.Entity<Achievement>()
            .HasOne(a => a.Player)
            .WithMany(p => p.Achievements)
            .HasForeignKey(a => a.PlayerId);

        // Configure Team relationships
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Game)
            .WithMany(g => g.Teams)
            .HasForeignKey(t => t.GameId);

        // Configure TeamPlayer (many-to-many)
        modelBuilder.Entity<TeamPlayer>()
            .HasKey(tp => new { tp.TeamId, tp.PlayerId });

        modelBuilder.Entity<TeamPlayer>()
            .HasOne(tp => tp.Team)
            .WithMany(t => t.TeamPlayers)
            .HasForeignKey(tp => tp.TeamId);

        modelBuilder.Entity<TeamPlayer>()
            .HasOne(tp => tp.Player)
            .WithMany(p => p.TeamPlayers)
            .HasForeignKey(tp => tp.PlayerId);

        // Seed data for Games
        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Name = "Space Battle", Genre = "Action" },
            new Game { Id = 2, Name = "Puzzle Quest", Genre = "Puzzle" },
            new Game { Id = 3, Name = "Racing World", Genre = "Racing" }
        );

        // Seed data for Players
        modelBuilder.Entity<Player>().HasData(
            new Player { Id = 1, Username = "PlayerOne" },
            new Player { Id = 2, Username = "GamerGirl" },
            new Player { Id = 3, Username = "Speedster" }
        );

        // Seed data for Leaderboards
        modelBuilder.Entity<Leaderboard>().HasData(
            new Leaderboard { Id = 1, GameId = 1, PlayerId = 1, Score = 1500 },
            new Leaderboard { Id = 2, GameId = 2, PlayerId = 2, Score = 2000 },
            new Leaderboard { Id = 3, GameId = 3, PlayerId = 3, Score = 1800 }
        );

        // Seed data for Achievements
        modelBuilder.Entity<Achievement>().HasData(
            new Achievement { Id = 1, Name = "First Blood", PlayerId = 1, GameId = 1 },
            new Achievement { Id = 2, Name = "Puzzle Master", PlayerId = 2, GameId = 2 },
            new Achievement { Id = 3, Name = "Speed Demon", PlayerId = 3, GameId = 3 }
        );

        // Seed data for Teams
        modelBuilder.Entity<Team>().HasData(
            new Team { Id = 1, Name = "Alpha Squad", GameId = 1 },
            new Team { Id = 2, Name = "Puzzle Warriors", GameId = 2 }
        );

        // Seed data for TeamPlayers (Many-to-Many)
        modelBuilder.Entity<TeamPlayer>().HasData(
            new TeamPlayer { TeamId = 1, PlayerId = 1 },
            new TeamPlayer { TeamId = 1, PlayerId = 3 },
            new TeamPlayer { TeamId = 2, PlayerId = 2 }
        );
    }

}
