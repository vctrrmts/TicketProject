using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Events.Queries.GetListEvents;

public class GetListEventsQueryValidator : AbstractValidator<GetListEventsQuery>
{
    public GetListEventsQueryValidator() 
    {
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
    }
}
