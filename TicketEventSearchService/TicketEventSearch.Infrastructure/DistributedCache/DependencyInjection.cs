using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketEventSearch.Application.Abstractions.Caches.Category;
using TicketEventSearch.Application.Abstractions.Caches.City;
using TicketEventSearch.Application.Abstractions.Caches.Event;
using TicketEventSearch.Application.Abstractions.Caches.Location;
using TicketEventSearch.Application.Abstractions.Caches.Seat;
using TicketEventSearch.Application.Abstractions.Caches.Ticket;
using TicketEventSearch.Infrastructure.DistributedCache.Categories;
using TicketEventSearch.Infrastructure.DistributedCache.Cities;
using TicketEventSearch.Infrastructure.DistributedCache.Events;
using TicketEventSearch.Infrastructure.DistributedCache.Locations;
using TicketEventSearch.Infrastructure.DistributedCache.Seats;
using TicketEventSearch.Infrastructure.DistributedCache.Tickets;

namespace TicketEventSearch.Infrastructure.DistributedCache;

public static class DependencyInjection
{
    public static IServiceCollection AddDistributedCacheServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        return services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetConnectionString("Redis");
                })
                .AddTransient<IEventCache, EventCache>()
                .AddTransient<IEventListCache, EventListCache>()
                .AddTransient<ITicketCache, TicketCache>()
                .AddTransient<ITicketListCache, TicketListCache>()
                .AddTransient<ICategoryCache, CategoryCache>()
                .AddTransient<ICategoryListCache, CategoryListCache>()
                .AddTransient<ICityCache, CityCache>()
                .AddTransient<ICityListCache, CityListCache>()
                .AddTransient<ILocationCache, LocationCache>()
                .AddTransient<ILocationListCache, LocationListCache>()
                .AddTransient<ISeatCache, SeatCache>()
                .AddTransient<ISeatListCache, SeatsListCache>()
                .AddTransient<RedisService>();

    }
}
