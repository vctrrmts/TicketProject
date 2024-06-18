using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.DTOs;
using TicketEventSearch.Application.Handlers.Seats.Commands.AddRangeSeats;
using TicketEventSearch.Application.Handlers.Seats.Commands.DeleteRangeSeats;
using TicketEventSearch.Application.Handlers.Seats.Queries.GetListSeats;

namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Seats search service
/// </summary>
[Authorize]
[Route("Search/Seats")]
[ApiController]
public class SeatsController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create range of seats. The command is sent from management service
    /// </summary>
    [HttpPost("AddSeats")]
    public async Task<IActionResult> AddRangeOfSeatsAsync(
        SeatForExportDto[] seats,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        AddRangeSeatsCommand addRangeSeatsCommand = new AddRangeSeatsCommand() { Seats = seats };
        var newSeats = await mediator.Send(addRangeSeatsCommand, cancellationToken);
        return Ok(newSeats);
    }

    /// <summary>
    /// Remove range of seats. The command is sent from management service
    /// </summary>
    [HttpPost("RemoveSeats")]
    public async Task<IActionResult> DeleteRangeOfSeatsAsync(
        SeatForExportDto[] seats,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        DeleteRangeSeatsCommand deleteRangeSeatsCommand = new DeleteRangeSeatsCommand() { Seats = seats };
        await mediator.Send(deleteRangeSeatsCommand, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of seats by SchemeId
    /// </summary>
    [AllowAnonymous]
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
