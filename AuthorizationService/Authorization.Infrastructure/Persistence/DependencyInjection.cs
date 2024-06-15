using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Authorization.Application.Abstractions.Persistence;
using Authorization.Domain;
using Common.Domain;

namespace Authorization.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
         return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
            .AddTransient<IBaseRepository<User>, BaseRepository<User>>()
            .AddTransient<IBaseRepository<RefreshToken>, BaseRepository<RefreshToken>>();

    }
}
