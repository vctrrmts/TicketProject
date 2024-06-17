using Auth.Application.Dtos;
using Authorization.Application.Abstractions.Persistence;
using Authorization.Application.Exceptions;
using Authorization.Domain;
using Common.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Application.Handlers.JwtToken.Command.CreateJwtTokenByRefreshToken;

public class CreateJwtTokenByRefreshTokenCommandHandler : IRequestHandler<CreateJwtTokenByRefreshTokenCommand, JwtTokenDto>
{
    private readonly IBaseRepository<User> _users;
    private readonly IBaseRepository<RefreshToken> _refreshTokens;
    private readonly IConfiguration _configuration;

    public CreateJwtTokenByRefreshTokenCommandHandler(
        IBaseRepository<User> users,
        IBaseRepository<RefreshToken> refreshTokens,
        IConfiguration configuration)
    {
        _users = users;
        _refreshTokens = refreshTokens;
        _configuration = configuration;
    }
    public async Task<JwtTokenDto> Handle(CreateJwtTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenFromDB = await _refreshTokens.SingleOrDefaultAsync(x => x.RefreshTokenId == request.RefreshToken, cancellationToken);
        if (refreshTokenFromDB is null)
        {
            throw new ForbiddenException();
        }

        var user = await _users.SingleAsync(x => x.UserId == refreshTokenFromDB.UserId, cancellationToken);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Login),
            new (ClaimTypes.NameIdentifier, user.UserId.ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var dateExpires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresMinutes"]));
        var tokenDescriptor = new JwtSecurityToken(_configuration["Jwt:Issuer"]!, _configuration["Jwt:Audience"]!
            , claims, expires: dateExpires, signingCredentials: credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return new JwtTokenDto
        {
            JwtToken = token,
            RefreshToken = refreshTokenFromDB.RefreshTokenId,
            Expires = dateExpires
        };
    }
}
