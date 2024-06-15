using MediatR;

namespace TicketControlService.Application.Handlers.Commands.VerifyTicket;

public class VerifyTicketCommand : IRequest <bool>
{
    public string HashFromQR { get; set; }
    public Guid EventId { get; set; }
}
