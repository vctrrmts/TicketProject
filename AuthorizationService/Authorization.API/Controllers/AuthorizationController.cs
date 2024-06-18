using Authorization.Application.Handlers.JwtToken.Command.CreateJwtToken;
using Authorization.Application.Handlers.JwtToken.Command.CreateJwtTokenByRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.API.Controllers;

/// <summary>
/// Authorization controller
/// </summary>
[Authorize]
[Route("Auth/Jwt")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    /// <summary>
    /// Create Jwt Token
    /// </summary>
    [AllowAnonymous]
    [HttpPost("CreateJwtToken")]
    public async Task<IActionResult> CreateJwtToken(
        CreateJwtTokenCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }

    /// <summary>
    /// Create Jwt Token by Refresh token
    /// </summary>
    [HttpPost("CreateJwtTokenByRefreshToken")]
    public async Task<IActionResult> CreateJwtToken(
        CreateJwtTokenByRefreshTokenCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }
}
