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

namespace TicketEventManagement.Application.Handlers.Cities.Commands.UpdateCity;

internal class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, GetCityDto>
{
    private readonly IBaseRepository<City> _cities;

    private readonly ICitiesRepository _citiesRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public UpdateCityCommandHandler(IBaseRepository<City> cities, ICitiesRepository citiesRepository, 
        IMapper mapper, ICurrentUserService currentUserService)
    {
        _cities = cities;
        _citiesRepository = citiesRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task<GetCityDto> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        City? city = await _cities.SingleOrDefaultAsync(x => x.CityId == request.CityId, cancellationToken);
        if (city == null)
        {
            throw new NotFoundException($"City with CityId = {request.CityId} not found");
        }

        city.UpdateName(request.Name);
        city.UpdateIsActive(request.IsActive);
        await _cities.UpdateAsync(city, cancellationToken);

        string token = _currentUserService.AccessToken;
        await _citiesRepository.UpdateCityAsync(city, token, cancellationToken);
        Log.Information("City updated " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetCityDto>(city);
    }
}
