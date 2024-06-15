using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Abstractions.Persistence;
using Notification.Application.Abstractions.Service;
using Notification.Domain;

namespace Notification.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
           {
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
           })
           .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
           .AddTransient<IBaseRepository<Event>, BaseRepository<Event>>();

    }
}
