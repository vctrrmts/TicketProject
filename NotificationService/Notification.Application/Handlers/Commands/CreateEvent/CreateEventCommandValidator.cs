using FluentValidation;

namespace Notification.Application.Handlers.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator() 
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
