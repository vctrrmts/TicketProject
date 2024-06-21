using FluentValidation;

namespace TicketEventSearch.Application.Handlers.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
