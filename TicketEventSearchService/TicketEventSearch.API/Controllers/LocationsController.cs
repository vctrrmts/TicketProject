using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.Handlers.Locations.Commands.CreateLocation;
using TicketEventSearch.Application.Handlers.Locations.Commands.DeleteLocation;
using TicketEventSearch.Application.Handlers.Locations.Commands.UpdateLocation;
using TicketEventSearch.Application.Handlers.Locations.Queries.GetListLocations;
using TicketEventSearch.Application.Handlers.Locations.Queries.GetLocation;

namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Locations search controller
/// </summary>
[Authorize]
[Route("Locations")]
[ApiController]
public class LocationsController : ControllerBase
{

    #region Commands
    /// <summary>
    /// Create location. The command is sent from management service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        CreateLocationCommand createLocationCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(createLocationCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Update location. The command is sent from management service
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(
        UpdateLocationCommand updateLocationCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(updateLocationCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Delete location. The command is sent from management service
    /// </summary>
    [HttpDelete("{locationId}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid locationId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteLocationCommand() { LocationId = locationId }, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of locations
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetListLocationQuery getListLocationQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var cities = await mediator.Send(getListLocationQuery, cancellationToken);
        return Ok(cities);
    }

    /// <summary>
    /// Get location by id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid locationId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var eventt = await mediator.Send(new GetLocationByIdQuery() { LocationId = locationId }, cancellationToken);
        return Ok(eventt);
    }
    #endregion
}
