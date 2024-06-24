using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.Handlers.Schemes.Commands.CreateScheme;
using TicketEventSearch.Application.Handlers.Schemes.Commands.UpdateScheme;

namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Schemes search controller
/// </summary>
[Authorize]
[Route("Search/Schemes")]
[ApiController]
public class SchemesController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create scheme. The command is sent from management service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        CreateSchemeCommand createSchemeCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(createSchemeCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Update scheme. The command is sent from management service
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(
        UpdateSchemeCommand updateSchemeCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(updateSchemeCommand, cancellationToken);
        return Ok();
    }
    #endregion
}
