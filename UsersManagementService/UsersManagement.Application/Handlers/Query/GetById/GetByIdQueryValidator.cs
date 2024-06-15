using FluentValidation;

namespace UsersManagement.Application.Handlers.Query.GetById;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(e => e.UserId).NotEmpty();
    }
}
