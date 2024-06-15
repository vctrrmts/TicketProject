using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Locations.Queries.GetListLocations;

public class GetListLocationQueryValidator : AbstractValidator<GetListLocationQuery>
{
    public GetListLocationQueryValidator() 
    {
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
        RuleFor(e => e.FreeText).MaximumLength(100);
    }
}
