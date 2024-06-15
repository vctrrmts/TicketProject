using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Schemes.Queries.GetScheme;

public class GetSchemeQuery : IRequest<GetSchemeDto>
{
    public Guid SchemeId { get; set; }
}
