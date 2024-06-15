using Microsoft.EntityFrameworkCore;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Scheme> Schemes { get; set; }
    public DbSet<Seat> Seats { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasKey(e => e.EventId);
        modelBuilder.Entity<Event>().Property(e => e.EventId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Event>().Property(e => e.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Event>().Property(e => e.Description).HasMaxLength(1000).IsRequired();
        modelBuilder.Entity<Event>().Property(e => e.UriMainImage).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<Event>().Navigation(t => t.Tickets).AutoInclude();
        modelBuilder.Entity<Event>().Navigation(t => t.Location).AutoInclude();
        modelBuilder.Entity<Event>().Navigation(t => t.Category).AutoInclude();

        modelBuilder.Entity<Ticket>().HasKey(t => t.TicketId);
        modelBuilder.Entity<Ticket>().Property(t => t.TicketId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Ticket>()
            .HasOne(v => v.Event)
            .WithMany(c => c.Tickets)
            .HasForeignKey(v => v.EventId);
        modelBuilder.Entity<Ticket>()
            .HasOne(v => v.Seat)
            .WithMany(c => c.Tickets)
            .HasForeignKey(v => v.SeatId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Ticket>().Navigation(t => t.Seat).AutoInclude();

        modelBuilder.Entity<City>().HasKey(c => c.CityId);
        modelBuilder.Entity<City>().Property(e => e.CityId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<City>().Property(c => c.Name).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<City>().HasIndex(c => c.Name).IsUnique();

        modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
        modelBuilder.Entity<Category>().Property(e => e.CategoryId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
        modelBuilder.Entity<Location>().Property(e => e.LocationId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Location>().Property(l => l.Name).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<Location>().Property(l => l.Address).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<Location>()
            .HasOne(l => l.City)
            .WithMany(c => c.Locations)
            .HasForeignKey(c => c.CityId);
        modelBuilder.Entity<Location>().Navigation(t => t.City).AutoInclude();
        modelBuilder.Entity<Location>().Navigation(t => t.Schemes).AutoInclude();

        modelBuilder.Entity<Scheme>().HasKey(s => s.SchemeId);
        modelBuilder.Entity<Scheme>().Property(e => e.SchemeId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Scheme>().Property(s=>s.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Scheme>().HasIndex(s => new { s.Name, s.SchemeId } ).IsUnique();
        modelBuilder.Entity<Scheme>()
            .HasOne(s => s.Location)
            .WithMany(l => l.Schemes)
            .HasForeignKey(l => l.LocationId);
        modelBuilder.Entity<Scheme>().Navigation(t => t.Seats).AutoInclude();

        modelBuilder.Entity<Seat>().HasKey(s => s.SeatId);
        modelBuilder.Entity<Seat>().Property(e => e.SeatId).HasDefaultValueSql("NEWID()");
        modelBuilder.Entity<Seat>().Property(s => s.Sector).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Seat>()
            .HasOne(s => s.Scheme)
            .WithMany(l => l.Seats)
            .HasForeignKey(l => l.SchemeId);

        base.OnModelCreating(modelBuilder);
    }
}
