using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.Location;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Locations.Commands.DeleteLocation;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly ICleanLocationCacheService _cleanLocationCacheService;

    public DeleteLocationCommandHandler(IBaseRepository<Location> locations, 
        ICleanLocationCacheService cleanLocationCacheService)
    {
        _locations = locations;
        _cleanLocationCacheService = cleanLocationCacheService;
    }

    public async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        Location? location = await _locations.SingleOrDefaultAsync(x => x.LocationId == request.LocationId, cancellationToken);
        if (location == null)
        {
            throw new NotFoundException($"Location with LocationId = {request.LocationId} not found");
        }

        location.UpdateIsActive(false);
        await _locations.UpdateAsync(location, cancellationToken);
        Log.Information("Location deleted " + JsonSerializer.Serialize(request));

        _cleanLocationCacheService.ClearAllLocationCaches();
    }
}
