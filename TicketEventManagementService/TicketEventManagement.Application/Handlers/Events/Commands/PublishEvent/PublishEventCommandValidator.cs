using FluentValidation;
using MediatR;

namespace TicketEventManagement.Application.Handlers.Events.Commands.PublishEvent;

public class PublishEventCommandValidator : AbstractValidator<PublishEventCommand>
{
    public PublishEventCommandValidator()
    {
        RuleFor(e=>e.EventId).NotEmpty();
    }
}
