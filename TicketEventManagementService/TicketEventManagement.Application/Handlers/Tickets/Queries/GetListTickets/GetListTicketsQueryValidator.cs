using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Tickets.Queries.GetListTickets;

public class GetListTicketsQueryValidator : AbstractValidator<GetListTicketsQuery>
{
    public GetListTicketsQueryValidator()
    {
        RuleFor(e => e.EventId).NotEmpty().When(e => e.EventId.HasValue);
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
    }
}
