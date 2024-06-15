using FluentValidation;

namespace UsersManagement.Application.Handlers.Query.GetList;

public class GetListQueryValidator : AbstractValidator<GetListQuery>
{
    public GetListQueryValidator()
    {
        RuleFor(e => e.Limit).GreaterThan(0).When(e => e.Limit.HasValue);
        RuleFor(e => e.Offset).GreaterThan(0).When(e => e.Offset.HasValue);
        RuleFor(e => e.LoginFreeText).MaximumLength(50);
    }
}
