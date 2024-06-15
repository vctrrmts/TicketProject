using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Cities.Queries.GetCity;

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, GetCityDto>
{
    private readonly IBaseRepository<City> _cities;

    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(IBaseRepository<City> cities, IMapper mapper)
    {
        _cities = cities;
        _mapper = mapper;
    }

    public async Task<GetCityDto> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        City? city = await _cities.SingleOrDefaultAsync(x => x.CityId == request.CityId, cancellationToken);

        if (city == null)
        {
            throw new NotFoundException($"City with id = {request.CityId} not found");
        }

        return _mapper.Map<GetCityDto>(city);
    }
}
