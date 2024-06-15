using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Categories.Queries.GetCategory;

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.CategoryById).NotEmpty();
    }
}
