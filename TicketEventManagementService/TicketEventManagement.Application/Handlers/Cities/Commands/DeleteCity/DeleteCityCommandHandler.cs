using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Cities.Commands.DeleteCity;

internal class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
{
    private readonly IBaseRepository<City> _cities;

    private readonly ICitiesRepository _citiesRepository;

    private readonly ICurrentUserService _currentUserService;

    public DeleteCityCommandHandler(IBaseRepository<City> cities, 
        ICitiesRepository citiesRepository, ICurrentUserService currentUserService) 
    {
        _cities = cities;
        _citiesRepository = citiesRepository;
        _currentUserService = currentUserService;
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

        string token = _currentUserService.AccessToken;
        await _citiesRepository.DeleteCityAsync(request.CityId, token, cancellationToken);
        Log.Information("City deleted" + JsonSerializer.Serialize(request));
    }
}
