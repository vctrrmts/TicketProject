using Microsoft.EntityFrameworkCore;
using UsersManagement.Domain;

namespace UsersManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(t => t.UserId);
        modelBuilder.Entity<User>().Property(r => r.UserId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<User>().Property(t => t.Login).HasMaxLength(50).IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
