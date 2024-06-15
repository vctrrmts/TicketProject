using Microsoft.EntityFrameworkCore;
using Notification.Domain;

namespace Notification.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<Event> NotifiedEvents { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Event>().HasKey(t => t.EventId);
        base.OnModelCreating(modelBuilder);
    }
}
