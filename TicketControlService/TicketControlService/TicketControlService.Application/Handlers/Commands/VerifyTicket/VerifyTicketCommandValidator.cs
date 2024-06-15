using FluentValidation;

namespace TicketControlService.Application.Handlers.Commands.VerifyTicket;

public class VerifyTicketCommandValidator : AbstractValidator<VerifyTicketCommand>
{
    public VerifyTicketCommandValidator() 
    {
        RuleFor(x=>x.HashFromQR).NotEmpty();
        RuleFor(x=>x.EventId).NotEmpty();
    }
}
