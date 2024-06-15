using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketControlService.Application.Abstractions.Persistence;
using TicketControlService.Domain;

namespace TicketControlService.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
           {
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
           })
           .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
           .AddTransient<IBaseRepository<Ticket>, BaseRepository<Ticket>>();

    }
}
