using Microsoft.EntityFrameworkCore;
using TicketControlService.Domain;

namespace TicketControlService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<Ticket> Tickets { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Ticket>().HasKey(t => t.TicketId);
        modelBuilder.Entity<Ticket>().Property(c => c.EventId).IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
