using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Schemes.Queries.GetListSchemes;

public class GetListSchemesQuery : IRequest<IReadOnlyCollection<GetSchemeDto>>
{
    public Guid? LocationId { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? FreeText { get; set; }
    public bool? IsActive { get; set; }
}
