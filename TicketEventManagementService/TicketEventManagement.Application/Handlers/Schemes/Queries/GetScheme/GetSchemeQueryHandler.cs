using AutoMapper;
using MediatR;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Schemes.Queries.GetScheme;

internal class GetSchemeQueryHandler : IRequestHandler<GetSchemeQuery, GetSchemeDto>
{
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly IMapper _mapper;

    public GetSchemeQueryHandler(IBaseRepository<Scheme> schemes, IMapper mapper)
    {
        _schemes = schemes;
        _mapper = mapper;
    }

    public async Task<GetSchemeDto> Handle(GetSchemeQuery request, CancellationToken cancellationToken)
    {
        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId, cancellationToken);
        if (scheme == null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        return _mapper.Map<GetSchemeDto>(scheme);
    }
}
