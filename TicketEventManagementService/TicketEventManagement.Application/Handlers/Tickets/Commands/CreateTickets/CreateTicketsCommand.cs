using MediatR;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.CreateTickets;

public class CreateTicketsCommand : IRequest<IReadOnlyCollection<GetTicketDto>>
{
    public Guid EventId { get; set; } = default!;
    public Guid SchemeId { get; set; } = default!;
    public double? Price { get; set; }
}
