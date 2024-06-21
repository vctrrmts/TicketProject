using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Events.Queries.GetEvents;

public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator()
    {
        RuleFor(e => e.CategoryId).NotEmpty().When(e => e.CategoryId.HasValue);
        RuleFor(e => e.LocationId).NotEmpty().When(e => e.LocationId.HasValue);
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
    }
}
