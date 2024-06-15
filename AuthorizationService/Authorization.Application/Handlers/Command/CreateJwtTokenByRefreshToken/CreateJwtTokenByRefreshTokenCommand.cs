using Auth.Application.Dtos;
using MediatR;

namespace Authorization.Application.Handlers.Command.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommand : IRequest<JwtTokenDto>
{
    public Guid RefreshToken { get; set; } = default!;
}
