using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Seats.Queries.GetListSeats;

public class GetListSeatsQueryValidator : AbstractValidator<GetListSeatsQuery>
{
    public GetListSeatsQueryValidator()
    {
        RuleFor(e => e.SchemeId).NotEmpty();
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
    }
}
