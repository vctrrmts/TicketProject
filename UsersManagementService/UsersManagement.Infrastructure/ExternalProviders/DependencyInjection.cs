using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Application.Abstractions.ExternalRepositories;

namespace UsersManagement.Infrastructure.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalRepositories(this IServiceCollection services)
    {
        return services.AddTransient<IUsersGRPCRepository, UsersGRPCRepository>();
    }
}