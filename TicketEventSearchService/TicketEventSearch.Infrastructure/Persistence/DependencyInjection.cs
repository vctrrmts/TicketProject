using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
         return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
            .AddTransient<IBaseRepository<Event>, BaseRepository<Event>>()
            .AddTransient<IBaseRepository<Ticket>, BaseRepository<Ticket>>()
            .AddTransient<IBaseRepository<City>, BaseRepository<City>>()
            .AddTransient<IBaseRepository<Location>, BaseRepository<Location>>()
            .AddTransient<IBaseRepository<Scheme>, BaseRepository<Scheme>>()
            .AddTransient<IBaseRepository<Seat>, BaseRepository<Seat>>()
            .AddTransient<IBaseRepository<Category>, BaseRepository<Category>>()
            .AddScoped<IDatabaseMigrator, DatabaseMigrator>();

    }
}
