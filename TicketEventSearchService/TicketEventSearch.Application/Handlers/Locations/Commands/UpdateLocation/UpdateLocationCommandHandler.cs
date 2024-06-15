using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Location;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.UpdateLocation;

internal class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly ICleanLocationCacheService _cleanLocationCacheService;

    public UpdateLocationCommandHandler(IBaseRepository<Location> locations, 
        ICleanLocationCacheService cleanLocationCacheService)
    {
        _locations = locations;
        _cleanLocationCacheService = cleanLocationCacheService;
    }
    public async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await _locations.SingleOrDefaultAsync(x => x.LocationId == request.LocationId, cancellationToken);
        if (location == null)
        {
            throw new NotFoundException($"Location with LocationId = {request.LocationId} not found");
        }

        location.UpdateName(request.Name);
        location.UpdateAddress(request.Address);
        location.UpdateLatitude(request.Latitude);
        location.UpdateLongitude(request.Longitude);
        location.UpdateIsActive(request.IsActive);

        await _locations.UpdateAsync(location, cancellationToken);
        Log.Information("Location updated " + JsonSerializer.Serialize(request));

        _cleanLocationCacheService.ClearAllLocationCaches();
    }
}
