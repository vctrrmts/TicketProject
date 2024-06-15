using Microsoft.Extensions.DependencyInjection;
using TicketControlService.Application.Abstractions.ExternalProviders;

namespace TicketControlService.Infrastructure.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<ITicketsRepository, TicketsRepository>();
    }
}