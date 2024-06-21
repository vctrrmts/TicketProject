using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Location;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly ICleanLocationCacheService _cleanLocationCacheService;

    public CreateLocationCommandHandler(IBaseRepository<Location> locations,
        ICleanLocationCacheService cleanLocationCacheService)
    {
        _locations = locations;
        _cleanLocationCacheService = cleanLocationCacheService;
    }

    public async Task Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var newLocation = new Location(request.LocationId, request.CityId, request.Name, 
            request.Address, request.Latitude, request.Longitude, request.IsActive);
        await _locations.AddAsync(newLocation, cancellationToken);
        Log.Information("Location added " + JsonSerializer.Serialize(request));

        _cleanLocationCacheService.ClearListLocationCaches();
    }
}
