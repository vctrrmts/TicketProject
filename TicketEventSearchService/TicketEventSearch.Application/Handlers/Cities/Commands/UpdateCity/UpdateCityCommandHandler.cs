using AutoMapper;
using MediatR;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Caches.City;
using Serilog;
using System.Text.Json;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.UpdateCity;

public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, GetCityDto>
{
    private readonly IBaseRepository<City> _cities;

    private readonly IMapper _mapper;

    private readonly ICleanCityCacheService _cleanCityCacheService;

    public UpdateCityCommandHandler(IBaseRepository<City> cities, IMapper mapper, 
        ICleanCityCacheService cleanCityCacheService)
    {
        _cities = cities;
        _mapper = mapper;
        _cleanCityCacheService = cleanCityCacheService;
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
        Log.Information("City updated " + JsonSerializer.Serialize(request));

        _cleanCityCacheService.ClearAllCityCaches();

        return _mapper.Map<GetCityDto>(city);
    }
}
