using FluentValidation;

namespace Authorization.Application.Handlers.Command.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommandValidator : AbstractValidator<CreateJwtTokenByRefreshTokenCommand>
{
    public CreateJwtTokenByRefreshTokenCommandValidator()
    {
        RuleFor(r => r.RefreshToken).NotEmpty();
    }
}
