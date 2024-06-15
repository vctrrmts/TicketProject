using FluentValidation;

namespace UsersManagement.Application.Handlers.Query.GetByLogin;

public class GetByLoginQueryValidator : AbstractValidator<GetByLoginQuery>
{
    public GetByLoginQueryValidator() 
    {
        RuleFor(x => x.Login).Length(3, 50);
    }
}
