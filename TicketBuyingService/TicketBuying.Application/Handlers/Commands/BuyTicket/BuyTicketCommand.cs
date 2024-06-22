using MediatR;

namespace TicketBuying.Application.Handlers.Commands.BuyTicket;

public class BuyTicketCommand : IRequest
{
    public Guid[] TicketIds { get; set; } = default!;

    public string Mail { get; set; } = default!;
}
