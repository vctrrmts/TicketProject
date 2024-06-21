using FluentValidation;
using TicketEventSearch.Application.ValidatorsExtensions;

namespace TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;

public class UpdateTicketStatusCommandValidator : AbstractValidator<UpdateTicketStatusCommand>
{
    public UpdateTicketStatusCommandValidator()
    {
        RuleFor(e => e.TicketId).NotEmpty();
        RuleFor(e => e.TicketStatusId).GreaterThanOrEqualTo(1).LessThanOrEqualTo(3);
    }
}
