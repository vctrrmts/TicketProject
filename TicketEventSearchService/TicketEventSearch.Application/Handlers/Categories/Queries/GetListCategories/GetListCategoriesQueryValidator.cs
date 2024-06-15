using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Categories.Queries.GetListCategories;

public class GetListCategoriesQueryValidator : AbstractValidator<GetListCategoriesQuery>
{
    public GetListCategoriesQueryValidator()
    {
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
        RuleFor(e => e.FreeText).MaximumLength(100);
    }
}
