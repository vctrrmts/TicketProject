using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Cities.Commands.CreateCity;

internal class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, GetCityDto>
{
    private readonly IBaseRepository<City> _cities;

    private readonly ICitiesRepository _citiesRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public CreateCityCommandHandler(IBaseRepository<City> cities, IMapper mapper, 
        ICitiesRepository citiesRepository, ICurrentUserService currentUserService)
    {
        _cities = cities;
        _citiesRepository = citiesRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetCityDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var newCity = new City(request.Name);
        newCity = await _cities.AddAsync(newCity, cancellationToken);

        string token = _currentUserService.AccessToken;
        await _citiesRepository.CreateCityAsync(newCity, token, cancellationToken);
        Log.Information("City added " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetCityDto>(newCity);
    }
}
