using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Locations.Queries.GetLocation;

internal class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationDto>
{
    private readonly IBaseRepository<Location> _locations;

    private readonly IMapper _mapper;

    public GetLocationByIdQueryHandler(IBaseRepository<Location> locations, IMapper mapper)
    {
        _locations = locations;
        _mapper = mapper;
    }
    public async Task<GetLocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        Location? location = await _locations.SingleOrDefaultAsync(x => x.LocationId == request.LocationId, cancellationToken);
        if (location == null)
        {
            throw new NotFoundException($"Location with LocationId = {request.LocationId} not found");
        }

        return _mapper.Map<GetLocationDto>(location);
    }
}
