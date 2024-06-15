using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketsByEventId;

public class GetTicketsByEventIdQueryValidator : AbstractValidator<GetTicketsByEventIdQuery>
{
    public GetTicketsByEventIdQueryValidator() 
    {
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
    }
}
