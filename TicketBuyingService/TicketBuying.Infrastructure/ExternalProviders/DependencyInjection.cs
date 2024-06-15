using Microsoft.Extensions.DependencyInjection;
using TicketBuying.Application.Abstractions.ExternalProviders;

namespace TicketBuying.Infrastructure.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<ITicketsRepository, TicketsRepository>();
    }
}