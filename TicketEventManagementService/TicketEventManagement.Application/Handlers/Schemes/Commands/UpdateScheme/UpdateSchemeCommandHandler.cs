using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommandHandler : IRequestHandler<UpdateSchemeCommand, GetSchemeDto>
{
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly ISchemesRepository _schemesRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public UpdateSchemeCommandHandler(IBaseRepository<Scheme> schemes,
        ISchemesRepository schemesRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _schemes = schemes;
        _schemesRepository = schemesRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetSchemeDto> Handle(UpdateSchemeCommand request, CancellationToken cancellationToken)
    {
        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId, cancellationToken);
        if (scheme == null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        scheme.UpdateName(request.Name);
        scheme.UpdateIsActive(request.IsActive);

        await _schemes.UpdateAsync(scheme, cancellationToken);

        string accessToken = _currentUserService.AccessToken;
        await _schemesRepository.UpdateSchemeAsync(scheme, accessToken, cancellationToken);
        Log.Information("Scheme updated " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetSchemeDto>(scheme);
    }
}
