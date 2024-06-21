using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Caches.City;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Cities.Commands.DeleteCity;

public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
{
    private readonly IBaseRepository<City> _cities;

    private readonly ICleanCityCacheService _cleanCityCacheService;

    public DeleteCityCommandHandler(IBaseRepository<City> cities, ICleanCityCacheService cleanCityCacheService) 
    {
        _cities = cities;
        _cleanCityCacheService = cleanCityCacheService;
    }
    public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        City? cityById = await _cities.SingleOrDefaultAsync(x => x.CityId == request.CityId, cancellationToken);
        if (cityById == null)
        {
            throw new NotFoundException($"City with CityId = {request.CityId} not found");
        }

        cityById.UpdateIsActive(false);

        await _cities.UpdateAsync(cityById, cancellationToken);
        Log.Information("City deleted " + JsonSerializer.Serialize(request));

        _cleanCityCacheService.ClearAllCityCaches();
    }
}
