using AutoMapper;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.BaseRealizations;
using TicketEventSearch.Application.Abstractions.Caches.Location;

namespace TicketEventSearch.Application.Handlers.Locations.Queries.GetLocation;

internal class GetLocationByIdQueryHandler : BaseCashedQuery<GetLocationByIdQuery, GetLocationDto>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IBaseRepository<Location> locations, 
        IMapper mapper, ILocationCache cache) : base(cache)
    {
        _locations = locations;
        _mapper = mapper;
    }

    public override async Task<GetLocationDto> SentQueryAsync(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        Location? location = await _locations.SingleOrDefaultAsync(x => x.LocationId == request.LocationId, cancellationToken);
        if (location == null)
        {
            throw new NotFoundException($"Location with LocationId = {request.LocationId} not found");
        }

        return _mapper.Map<GetLocationDto>(location);
    }
}
