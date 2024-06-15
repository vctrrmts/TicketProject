using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Tickets.Commands.CreateTickets;

public class CreateTicketsCommandValidator : AbstractValidator<CreateTicketsCommand>
{
    public CreateTicketsCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.SchemeId).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0).When(x=>x.Price.HasValue);
    }
}
