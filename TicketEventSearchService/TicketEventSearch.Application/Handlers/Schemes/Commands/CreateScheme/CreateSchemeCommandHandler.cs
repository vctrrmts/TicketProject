using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventSearch.Application.Abstractions.Persistence;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Schemes.Commands.CreateScheme;

internal class CreateSchemeCommandHandler : IRequestHandler<CreateSchemeCommand>
{
    private readonly IBaseRepository<Scheme> _schemes;

    public CreateSchemeCommandHandler(IBaseRepository<Scheme> schemes)
    {
        _schemes = schemes;
    }

    public async Task Handle(CreateSchemeCommand request, CancellationToken cancellationToken)
    {
        var newScheme = new Scheme(request.SchemeId, request.LocationId, request.Name);
        await _schemes.AddAsync(newScheme, cancellationToken);
        Log.Information("Scheme added " + JsonSerializer.Serialize(request));
    }
}
