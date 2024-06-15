using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketBuying.Application.Abstractions.Persistence;
using TicketBuying.Application.Abstractions.Service;
using TicketBuying.Domain;
using TicketBuying.Infrastructure.Mq;

namespace TicketBuying.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
         return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
            .AddTransient<IBaseRepository<BuyedTicket>, BaseRepository<BuyedTicket>>()
            .AddTransient<IMqService, MqService>();

    }
}
