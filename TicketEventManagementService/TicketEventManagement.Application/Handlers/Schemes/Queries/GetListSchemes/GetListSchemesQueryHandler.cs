using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Schemes.Queries.GetListSchemes;

internal class GetListSchemesQueryHandler : IRequestHandler<GetListSchemesQuery, IReadOnlyCollection<GetSchemeDto>>
{
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly IMapper _mapper;

    public GetListSchemesQueryHandler(IBaseRepository<Scheme> schemes, IMapper mapper)
    {
        _schemes = schemes;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetSchemeDto>> Handle(GetListSchemesQuery request, 
        CancellationToken cancellationToken)
    {
        var schemes = await _schemes.GetListAsync(
            request.Offset,
            request.Limit,
            e => (string.IsNullOrWhiteSpace(request.FreeText) || e.Name.Contains(request.FreeText))
            && ((request.LocationId == null) || (e.LocationId == request.LocationId))
            && ((request.IsActive == null) || (e.IsActive == request.IsActive)),
            x => x.Name,
            false,
            cancellationToken);

        return _mapper.Map<IReadOnlyCollection<GetSchemeDto>>(schemes);
    }
}
