using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Locations.Commands.CreateLocation;

internal class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, GetLocationDto>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly ILocationsRepository _locationsRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public CreateLocationCommandHandler(IBaseRepository<Location> locations, 
        ILocationsRepository locationsRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _locations = locations;
        _locationsRepository = locationsRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetLocationDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var newLocation = new Location(request.CityId, request.Name, request.Address, request.Latitude, request.Longitude);
        newLocation = await _locations.AddAsync(newLocation, cancellationToken);

        string accessToken = _currentUserService.AccessToken;
        await _locationsRepository.CreateLocationAsync(newLocation, accessToken, cancellationToken);
        Log.Information("Location added " + JsonSerializer.Serialize(request));
        return _mapper.Map<GetLocationDto>(newLocation);
    }
}
