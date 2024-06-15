using FluentValidation;

namespace TicketEventManagement.Application.Handlers.Schemes.Queries.GetListSchemes;

public class GetListSchemesQueryValidator : AbstractValidator<GetListSchemesQuery>
{
    public GetListSchemesQueryValidator()
    {
        RuleFor(e => e.LocationId).NotEmpty().When(e => e.LocationId.HasValue); ;
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
        RuleFor(e => e.FreeText).MaximumLength(100);
    }
}
