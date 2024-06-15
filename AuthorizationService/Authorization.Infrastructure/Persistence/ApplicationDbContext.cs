using Microsoft.EntityFrameworkCore;
using Authorization.Domain;
using Common.Domain;

namespace Authorization.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<User>().Property(u => u.UserId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<User>().Property(u => u.Login).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();

        modelBuilder.Entity<RefreshToken>().HasKey(r => r.RefreshTokenId);
        modelBuilder.Entity<RefreshToken>().Property(r => r.RefreshTokenId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<RefreshToken>()
            .HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(u => u.UserId);

        base.OnModelCreating(modelBuilder);
    }
}
