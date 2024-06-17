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
[Route("auth")]
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
    /// <param name="command"></param>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("CreateJwtTokenByRefreshToken")]
    public async Task<IActionResult> CreateJwtToken(
        CreateJwtTokenByRefreshTokenCommand command,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(command, cancellationToken));
    }
}
