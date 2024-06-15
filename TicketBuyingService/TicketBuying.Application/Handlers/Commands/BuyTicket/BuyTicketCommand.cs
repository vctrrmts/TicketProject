using MediatR;
using TicketBuying.Domain;

namespace TicketBuying.Application.Handlers.Commands.BuyTicket;

public class BuyTicketCommand : IRequest
{
    public Guid TicketId { get; set; }

    public string Mail { get; set; } = default!;
}
