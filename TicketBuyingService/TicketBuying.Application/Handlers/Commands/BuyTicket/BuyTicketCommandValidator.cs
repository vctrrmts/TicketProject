using FluentValidation;

namespace TicketBuying.Application.Handlers.Commands.BuyTicket;

public class BuyTicketCommandValidator : AbstractValidator<BuyTicketCommand>
{
    public BuyTicketCommandValidator() 
    {
        RuleFor(b=>b.TicketIds).NotEmpty();
        RuleFor(b=>b.Mail).NotEmpty();
    }
}
