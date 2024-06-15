using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Schemes.Commands.DeleteScheme;

public class DeleteSchemeCommandHandler : IRequestHandler<DeleteSchemeCommand>
{
    private readonly IBaseRepository<Scheme> _schemes;

    private readonly ISchemesRepository _schemesRepository;

    private readonly ICurrentUserService _currentUserService;

    public DeleteSchemeCommandHandler(IBaseRepository<Scheme> schemes, 
        ISchemesRepository schemesRepository, ICurrentUserService currentUserService)
    {
        _schemes = schemes;
        _schemesRepository = schemesRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(DeleteSchemeCommand request, CancellationToken cancellationToken)
    {
        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId, cancellationToken);
        if (scheme == null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        scheme.UpdateIsActive(false);
        await _schemes.UpdateAsync(scheme);

        string accessToken = _currentUserService.AccessToken;
        await _schemesRepository.UpdateSchemeAsync(scheme, accessToken, cancellationToken);
        Log.Information("Scheme deleted " + JsonSerializer.Serialize(request));
    }
}
