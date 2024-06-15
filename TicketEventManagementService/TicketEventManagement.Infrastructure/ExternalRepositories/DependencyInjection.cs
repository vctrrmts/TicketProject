using Microsoft.Extensions.DependencyInjection;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Infrastructure.ExternalRepositories.Relizations;

namespace TicketEventManagement.Infrastructure.ExternalRepositories;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<ICitiesRepository, CitiesRepository>()
                       .AddTransient<ILocationsRepository, LocationsRepository>()
                       .AddTransient<ISeatsRepository, SeatsRepository>()
                       .AddTransient<ISchemesRepository, SchemesRepository>()
                       .AddTransient<IEventsRepository, EventsRepository>()
                       .AddTransient<ICategoryRepository, CategoryRepository>();
    }
}