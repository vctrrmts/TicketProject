using FluentValidation;

namespace TicketBuying.Application.Handlers.Queries.GetTicketsByEventId;

public class GetTicketsByEventIdQueryValidator : AbstractValidator<GetTicketsByEventIdQuery>
{
    public GetTicketsByEventIdQueryValidator() 
    {
        RuleFor(x=>x.EventId).NotEmpty();
    }
}
