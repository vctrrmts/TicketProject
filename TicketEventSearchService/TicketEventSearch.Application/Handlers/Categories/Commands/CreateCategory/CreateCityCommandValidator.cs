using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Name).Length(1, 30).NotEmpty();
    }
}
