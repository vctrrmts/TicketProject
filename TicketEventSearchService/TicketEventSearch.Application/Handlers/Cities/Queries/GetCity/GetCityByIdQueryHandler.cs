using AutoMapper;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.Abstractions.Caches.City;

namespace TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;

public class GetCityByIdQueryHandler : BaseCashedQuery<GetCityByIdQuery, GetCityDto>
{
    private readonly IBaseRepository<City> _cities;

    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(IBaseRepository<City> cities, IMapper mapper, ICityCache cache) : base(cache)
    {
        _cities = cities;
        _mapper = mapper;
    }

    public override async Task<GetCityDto> SentQueryAsync(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        City? city = await _cities.SingleOrDefaultAsync(x => x.CityId == request.CityId, cancellationToken);

        if (city == null)
        {
            throw new NotFoundException($"City with id = {request.CityId} not found");
        }

        return _mapper.Map<GetCityDto>(city);
    }
}
