using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Domain.DbModels;

namespace RockPaperScissors.DataAccess;
public class AppDbContext : DbContext {

    public DbSet<GameTransactions> GameTransactionsDb { get; set; }
    public DbSet<Users> UserDb { get; set; }
    public DbSet<MatchHistory> MatchHistoryDb { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RockPaperScissorsDb;Username=postgres;Password=7204");
    }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {

        modelBuilder.Entity<GameTransactions>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<GameTransactions>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);


        modelBuilder.Entity<MatchHistory>()
            .HasOne(m => m.Player1)
            .WithMany()  
            .HasForeignKey(m => m.Player1Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MatchHistory>()
            .HasOne(m => m.Player2)
            .WithMany()
            .HasForeignKey(m => m.Player2Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MatchHistory>()
            .HasOne(m => m.Winner)
            .WithMany()
            .HasForeignKey(m => m.WinnerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MatchHistory>()
               .HasIndex(m => new { m.Player1Id, m.Player2Id, m.CreatedAt })
               .IsUnique();

        modelBuilder.Entity<Users>().HasIndex(u => u.UserName).IsUnique();
        modelBuilder.Entity<MatchHistory>().HasIndex(p => p.Player1Id);
        modelBuilder.Entity<MatchHistory>().HasIndex(p => p.Player2Id);
        modelBuilder.Entity<GameTransactions>().HasIndex(p => p.UserId);

    }
}
