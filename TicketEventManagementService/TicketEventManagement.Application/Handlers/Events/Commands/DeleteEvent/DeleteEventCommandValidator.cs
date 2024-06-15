using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Events.Commands.DeleteEvent;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
