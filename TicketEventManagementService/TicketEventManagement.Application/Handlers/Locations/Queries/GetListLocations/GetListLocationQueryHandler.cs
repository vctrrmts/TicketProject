using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Locations.Queries.GetListLocations;

internal class GetListLocationQueryHandler : IRequestHandler<GetListLocationQuery, IReadOnlyCollection<GetLocationDto>>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly IMapper _mapper;

    public GetListLocationQueryHandler(IBaseRepository<Location> locations, IMapper mapper)
    {
        _locations = locations;
        _mapper = mapper;
    }
    public async Task<IReadOnlyCollection<GetLocationDto>> Handle(GetListLocationQuery request, 
        CancellationToken cancellationToken)
    {
        var locations = await _locations.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && ((request.IsActive == null) || (e.IsActive == request.IsActive)),
            x => x.Name,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetLocationDto>>(locations);
    }
}
