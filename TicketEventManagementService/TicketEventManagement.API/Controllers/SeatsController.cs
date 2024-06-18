using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventManagement.Application.Handlers.Seats.Commands.AddRangeSeats;
using TicketEventManagement.Application.Handlers.Seats.Commands.DeleteRangeSeats;
using TicketEventManagement.Application.Handlers.Seats.Queries.GetListSeats;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Scheme's seats management controller
/// </summary>
[Authorize]
[Route("Manage/Seats")]
[ApiController]
public class SeatsController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Add range of seats to scheme with sending command to Search service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddRangeOfSeatsAsync(
        AddRangeSeatsCommand addRangeSeatsCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newSeats = await mediator.Send(addRangeSeatsCommand, cancellationToken);
        return Ok(newSeats);
    }

    /// <summary>
    /// Remove range of seats of scheme with sending command to Search service
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteRangeOfSeatsAsync(
        DeleteRangeSeatsCommand deleteRangeSeatsCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(deleteRangeSeatsCommand, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of schemes
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetListSeatsQuery getListSeatsQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var seats = await mediator.Send(getListSeatsQuery, cancellationToken);
        return Ok(seats);
    }
    #endregion
}
