using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TicketEventSearch.Application.Behaviors;
using TicketEventSearch.Application.Caches.Category;
using TicketEventSearch.Application.Caches.City;
using TicketEventSearch.Application.Caches.Event;
using TicketEventSearch.Application.Caches.Location;
using TicketEventSearch.Application.Caches.Seat;
using TicketEventSearch.Application.Caches.Ticket;

namespace TicketEventSearch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddTransient<ICleanEventCacheService, CleanEventCacheService>()
            .AddTransient<ICleanTicketCacheService, CleanTicketCacheService>()
            .AddTransient<ICleanCategoryCacheService, CleanCategoryCacheService>()
            .AddTransient<ICleanCityCacheService, CleanCityCacheService>()
            .AddTransient<ICleanLocationCacheService, CleanLocationCacheService>()
            .AddTransient<ICleanSeatCacheService, CleanSeatCacheService>()
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>));
    }
}
