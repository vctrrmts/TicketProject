using Auth.Application.Dtos;
using MediatR;

namespace Authorization.Application.Handlers.JwtToken.Command.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommand : IRequest<JwtTokenDto>
{
    public Guid RefreshToken { get; set; } = default!;
}
