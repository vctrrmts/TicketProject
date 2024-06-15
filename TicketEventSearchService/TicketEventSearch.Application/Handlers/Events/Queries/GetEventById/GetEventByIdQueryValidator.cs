using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Events.Queries.GetEventById;

public class GetEventByIdQueryValidator : AbstractValidator<GetEventByIdQuery>
{
    public GetEventByIdQueryValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}
