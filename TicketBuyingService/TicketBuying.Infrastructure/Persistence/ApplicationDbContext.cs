using Microsoft.EntityFrameworkCore;
using TicketBuying.Domain;

namespace TicketBuying.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<BuyedTicket> Tickets { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<BuyedTicket>().HasKey(t => t.TicketId);
        modelBuilder.Entity<BuyedTicket>().Property(c => c.EventId).IsRequired();
        modelBuilder.Entity<BuyedTicket>().Property(c => c.Price).IsRequired();
        modelBuilder.Entity<BuyedTicket>().Property(c => c.ClientMail).HasMaxLength(100).IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
