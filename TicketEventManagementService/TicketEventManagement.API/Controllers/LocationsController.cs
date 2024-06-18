using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Handlers.Locations.Commands.CreateLocation;
using TicketEventManagement.Application.Handlers.Locations.Commands.DeleteLocation;
using TicketEventManagement.Application.Handlers.Locations.Commands.UpdateLocation;
using TicketEventManagement.Application.Handlers.Locations.Queries.GetListLocations;
using TicketEventManagement.Application.Handlers.Locations.Queries.GetLocation;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Locations management controller
/// </summary>
[Authorize]
[Route("Manage/Locations")]
[ApiController]
public class LocationsController : ControllerBase
{

    #region Commands
    /// <summary>
    /// Create location with sending command to Search service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateLocationAsync(
        CreateLocationCommand createLocationCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newLocation = await mediator.Send(createLocationCommand, cancellationToken);
        return Created("/Locations/" + newLocation.LocationId, newLocation);
    }

    /// <summary>
    /// Update location with sending command to Search service
    /// </summary>
    [HttpPut("{locationId}")]
    public async Task<IActionResult> UpdateLocationAsync(
        [FromRoute] Guid locationId,
        UpdateLocationCommandDto dto,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        UpdateLocationCommand updateLocationCommand = new UpdateLocationCommand()
        {
            LocationId = locationId,
            Name = dto.Name,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            IsActive = dto.IsActive,
        };
        var updatedLocation = await mediator.Send(updateLocationCommand, cancellationToken);
        return Ok(updatedLocation);
    }

    /// <summary>
    /// Delete location with sending command to Search service
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
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetListLocationQuery getListLocationQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var locations = await mediator.Send(getListLocationQuery, cancellationToken);
        return Ok(locations);
    }

    /// <summary>
    /// Get location by id
    /// </summary>
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid locationId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var location = await mediator.Send(new GetLocationByIdQuery() { LocationId = locationId }, cancellationToken);
        return Ok(location);
    }
    #endregion
}
