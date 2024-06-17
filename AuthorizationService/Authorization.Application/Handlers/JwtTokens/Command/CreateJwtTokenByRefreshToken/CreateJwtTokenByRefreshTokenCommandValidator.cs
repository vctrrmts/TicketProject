using FluentValidation;

namespace Authorization.Application.Handlers.JwtToken.Command.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommandValidator : AbstractValidator<CreateJwtTokenByRefreshTokenCommand>
{
    public CreateJwtTokenByRefreshTokenCommandValidator()
    {
        RuleFor(r => r.RefreshToken).NotEmpty();
    }
}
