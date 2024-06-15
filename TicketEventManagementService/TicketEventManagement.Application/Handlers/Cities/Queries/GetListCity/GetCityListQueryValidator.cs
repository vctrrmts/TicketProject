using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Cities.Queries.GetListCity;

public class GetCityListQueryValidator : AbstractValidator<GetCityListQuery>
{
    public GetCityListQueryValidator()
    {
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
        RuleFor(e => e.FreeText).MaximumLength(100);
    }
}
