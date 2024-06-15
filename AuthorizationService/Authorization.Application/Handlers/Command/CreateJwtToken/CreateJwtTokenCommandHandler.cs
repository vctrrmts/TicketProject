using Auth.Application.Dtos;
using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Abstractions.Persistence;
using Authorization.Application.Exceptions;
using Authorization.Application.Utils;
using Authorization.Domain;
using Common.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Application.Handlers.Command.CreateJwtToken
{
    public class CreateJwtTokenCommandHandler : IRequestHandler<CreateJwtTokenCommand, JwtTokenDto>
    {
        private readonly IBaseRepository<User> _users;
        private readonly IBaseRepository<RefreshToken> _refreshTokens;
        private readonly IUsersProvider _usersProvider;
        private readonly IConfiguration _configuration;

        public CreateJwtTokenCommandHandler(
            IBaseRepository<User> users,
            IBaseRepository<RefreshToken> refreshTokens,
            IUsersProvider usersProvider,
            IConfiguration configuration)
        {
            _users = users;
            _refreshTokens = refreshTokens;
            _usersProvider = usersProvider;
            _configuration = configuration;
        }

        public async Task<JwtTokenDto> Handle(CreateJwtTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _users.SingleOrDefaultAsync(x => x.Login == request.Login.Trim(), cancellationToken);

            if (user is null) 
            {
                user = await _usersProvider.GetUserByLoginAsync(request.Login, cancellationToken);

                if (user is null)
                {
                    throw new NotFoundException($"User with login {request.Login} doesn't exist");
                }

                user.PasswordHash = PasswordHashUtil.HashPassword(request.Password);
                await _users.AddAsync(user);
            }
            else if (!PasswordHashUtil.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new ForbiddenException();
            }


            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, request.Login ),
                new (ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var dateExpires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresMinutes"]));
            var tokenDescriptor = new JwtSecurityToken(_configuration["Jwt:Issuer"]!, _configuration["Jwt:Audience"]!
                , claims, expires: dateExpires, signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            var refreshToken = await _refreshTokens.AddAsync(new RefreshToken() { UserId = user.UserId }, cancellationToken);

            return new JwtTokenDto
            {
                JwtToken = token,
                RefreshToken = refreshToken.RefreshTokenId,
                Expires = dateExpires
            };
        }
    }
}
