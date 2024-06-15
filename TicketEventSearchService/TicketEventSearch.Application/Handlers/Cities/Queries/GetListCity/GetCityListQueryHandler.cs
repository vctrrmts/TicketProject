using AutoMapper;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.Abstractions.Caches.City;

namespace TicketEventSearch.Application.Handlers.Cities.Queries.GetListCity;

public class GetCityListQueryHandler : BaseCashedQuery<GetCityListQuery, IReadOnlyCollection<GetCityDto>>
{
    private readonly IBaseRepository<City> _cities;
    private readonly IMapper _mapper;

    public GetCityListQueryHandler(IBaseRepository<City> cities, IMapper mapper, ICityListCache cache) : base(cache)
    {
        _cities = cities;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<GetCityDto>> SentQueryAsync(GetCityListQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cities.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && e.IsActive == true,
            x => x.CityId,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetCityDto>>(cities);
    }
}
