using AutoMapper;
using MediatR;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Caches.City;
using Serilog;
using System.Text.Json;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.CreateCity;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, GetCityDto>
{
    private readonly IBaseRepository<City> _cities;

    private readonly IMapper _mapper;

    private readonly ICleanCityCacheService _cleanCityCacheService;

    public CreateCityCommandHandler(IBaseRepository<City> cities, IMapper mapper,
        ICleanCityCacheService cleanCityCacheService)
    {
        _cities = cities;
        _mapper = mapper;
        _cleanCityCacheService = cleanCityCacheService;
    }

    public async Task<GetCityDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var newCity = new City(request.CityId ,request.Name, request.IsActive);
        newCity = await _cities.AddAsync(newCity, cancellationToken);
        Log.Information("City added " + JsonSerializer.Serialize(request));

        _cleanCityCacheService.ClearListCityCaches();

        return _mapper.Map<GetCityDto>(newCity);
    }
}
