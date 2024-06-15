using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.DeleteLocation;

internal class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly ILocationsRepository _locationsRepository;

    private readonly ICurrentUserService _currentUserService;

    public DeleteLocationCommandHandler(IBaseRepository<Location> locations, 
        ILocationsRepository locationsRepository, ICurrentUserService currentUserService)
    {
        _locations = locations;
        _locationsRepository = locationsRepository;
        _currentUserService = currentUserService;
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

        string accessToken = _currentUserService.AccessToken;
        await _locationsRepository.DeleteLocationAsync(request.LocationId, accessToken, cancellationToken);
        Log.Information("Location deleted " + JsonSerializer.Serialize(request));

    }
}
