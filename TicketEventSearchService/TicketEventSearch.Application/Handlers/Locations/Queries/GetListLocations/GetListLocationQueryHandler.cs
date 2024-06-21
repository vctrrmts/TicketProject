using AutoMapper;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.Abstractions.Caches.Location;

namespace TicketEventSearch.Application.Handlers.Locations.Queries.GetListLocations;

public class GetListLocationQueryHandler : BaseCashedQuery<GetListLocationQuery, IReadOnlyCollection<GetLocationDto>>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly IMapper _mapper;

    public GetListLocationQueryHandler(IBaseRepository<Location> locations, 
        IMapper mapper, ILocationListCache cache) : base(cache)
    {
        _locations = locations;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<GetLocationDto>> SentQueryAsync(GetListLocationQuery request, CancellationToken cancellationToken)
    {
        var locations = await _locations.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && e.IsActive == true,
            x => x.Name,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetLocationDto>>(locations);
    }
}
