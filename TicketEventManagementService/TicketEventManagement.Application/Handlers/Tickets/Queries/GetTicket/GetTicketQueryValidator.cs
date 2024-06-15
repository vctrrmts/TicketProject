using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Tickets.Queries.GetTicket;

public class GetTicketQueryValidator : AbstractValidator<GetTicketQuery>
{
    public GetTicketQueryValidator() 
    {
        RuleFor(e => e.TicketId).NotEmpty();
    }
}
