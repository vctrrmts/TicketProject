using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.CreateScheme;

internal class CreateSchemeCommandHandler : IRequestHandler<CreateSchemeCommand, GetSchemeDto>
{
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly ISchemesRepository _schemesRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public CreateSchemeCommandHandler(IBaseRepository<Scheme> schemes, 
        ISchemesRepository schemesRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _schemes = schemes;
        _schemesRepository = schemesRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetSchemeDto> Handle(CreateSchemeCommand request, CancellationToken cancellationToken)
    {
        var newScheme = new Scheme(request.LocationId, request.Name);
        newScheme = await _schemes.AddAsync(newScheme, cancellationToken);

        string accessToken = _currentUserService.AccessToken;
        await _schemesRepository.CreateSchemeAsync(newScheme, accessToken, cancellationToken);
        Log.Information("Scheme added " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetSchemeDto>(newScheme);
    }
}
