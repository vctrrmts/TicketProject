using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.LocationId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 100).NotEmpty();
        RuleFor(x => x.Description).Length(1, 1000).NotEmpty();
        RuleFor(x => x.UriMainImage).Length(1, 200).NotEmpty();
        RuleFor(x => x.DateTimeEventStart).NotNull();
        RuleFor(x => x.DateTimeEventEnd).NotNull();
        RuleFor(x => x).NotNull();
    }
}
