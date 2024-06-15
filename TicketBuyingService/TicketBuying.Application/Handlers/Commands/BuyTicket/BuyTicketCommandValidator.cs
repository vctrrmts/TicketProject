using FluentValidation;

namespace TicketBuying.Application.Handlers.Commands.BuyTicket;

public class BuyTicketCommandValidator : AbstractValidator<BuyTicketCommand>
{
    public BuyTicketCommandValidator() 
    {
        RuleFor(b=>b.TicketId).NotEmpty();
        RuleFor(b=>b.Mail).NotEmpty();
    }
}
