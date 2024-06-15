using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Handlers.Schemes.Commands.CreateScheme;
using TicketEventManagement.Application.Handlers.Schemes.Commands.DeleteScheme;
using TicketEventManagement.Application.Handlers.Schemes.Commands.UpdateScheme;
using TicketEventManagement.Application.Handlers.Schemes.Queries.GetListSchemes;
using TicketEventManagement.Application.Handlers.Schemes.Queries.GetScheme;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Schemes management controller
/// </summary>
[Authorize]
[Route("Schemes")]
[ApiController]
public class SchemesController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create scheme with sending to Search service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        CreateSchemeCommand createSchemeCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newScheme = await mediator.Send(createSchemeCommand, cancellationToken);
        return Created("/Schemes/" + newScheme.SchemeId, newScheme);
    }

    /// <summary>
    /// Update scheme with sending to Search service
    /// </summary>
    [HttpPut("{schemeId}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid schemeId,
        UpdateSchemeCommandDto dto,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        UpdateSchemeCommand updateSchemeCommand = new UpdateSchemeCommand()
        {
            SchemeId = schemeId,
            Name = dto.Name,
            IsActive = dto.IsActive,
        };
        var updatedScheme = await mediator.Send(updateSchemeCommand, cancellationToken);
        return Ok(updatedScheme);
    }

    /// <summary>
    /// Delete scheme with sending to Search service
    /// </summary>
    [HttpDelete("{schemeId}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid schemeId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteSchemeCommand() { SchemeId = schemeId }, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get schemes list
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetListSchemesQuery getListSchemesQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var schemes = await mediator.Send(getListSchemesQuery, cancellationToken);
        return Ok(schemes);
    }

    /// <summary>
    /// Get scheme by id
    /// </summary>
    [HttpGet("{schemeId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid schemeId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var scheme = await mediator.Send(new GetSchemeQuery() { SchemeId = schemeId }, cancellationToken);
        return Ok(scheme);
    }
    #endregion
}
