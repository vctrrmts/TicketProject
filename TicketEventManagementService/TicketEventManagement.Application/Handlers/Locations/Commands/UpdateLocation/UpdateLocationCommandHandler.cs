using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.UpdateLocation;

internal class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, GetLocationDto>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly ILocationsRepository _locationsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public UpdateLocationCommandHandler(IBaseRepository<Location> locations,
        ILocationsRepository locationsRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _locations = locations;
        _locationsRepository = locationsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task<GetLocationDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
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

        string accessToken = _currentUserService.AccessToken;
        await _locationsRepository.UpdateLocationAsync(location, accessToken, cancellationToken);
        Log.Information("Location updated " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetLocationDto>(location);
    }
}
