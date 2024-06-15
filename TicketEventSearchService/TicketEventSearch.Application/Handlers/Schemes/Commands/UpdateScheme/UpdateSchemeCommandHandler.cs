using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Application.Exceptions;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Schemes.Commands.UpdateScheme;

public class UpdateSchemeCommandHandler : IRequestHandler<UpdateSchemeCommand>
{
    private readonly IBaseRepository<Scheme> _schemes;

    public UpdateSchemeCommandHandler(IBaseRepository<Scheme> schemes)
    {
        _schemes = schemes;
    }

    public async Task Handle(UpdateSchemeCommand request, CancellationToken cancellationToken)
    {
        Scheme? scheme = await _schemes.SingleOrDefaultAsync(x => x.SchemeId == request.SchemeId, cancellationToken);
        if (scheme == null)
        {
            throw new NotFoundException($"Scheme with SchemeId = {request.SchemeId} not found");
        }

        scheme.UpdateName(request.Name);
        scheme.UpdateIsActive(request.IsActive);

        await _schemes.UpdateAsync(scheme, cancellationToken);
        Log.Information("Scheme updated " + JsonSerializer.Serialize(request));
    }
}
