using MediatR;
using TicketBuying.Domain;

namespace TicketBuying.Application.Handlers.Commands.VerifyTicketCommand;

public class VerifyTicketCommand : IRequest<BuyedTicket>
{
    public string HashGuid { get; set; } = default!;
}
