using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketById;

public class GetTicketByIdQueryValidator : AbstractValidator<GetTicketByIdQuery>
{
    public GetTicketByIdQueryValidator()
    {
        RuleFor(x => x.TicketId).NotEmpty();
    }
}
