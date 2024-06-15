using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.Handlers.Tickets.Commands.UpdateTicketStatus;
using TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketById;
using TicketEventSearch.Application.Handlers.Tickets.Queries.GetTicketsByEventId;

namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Tickets search controller
/// </summary>
[Authorize]
[Route("Tickets")]
[ApiController]
public class TicketsController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Update ticket status.  The command is sent from ticket buying service or from website
    /// </summary>
    [AllowAnonymous]
    [HttpPut("Status")]
    public async Task<IActionResult> UpdateStatusAsync(
        UpdateTicketStatusCommand updateTicketStatusCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var ticket = await mediator.Send(updateTicketStatusCommand, cancellationToken);
        return Ok(ticket);
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get tickets by EventId
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetTicketsByEventIdQuery getTicketsByEventId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var tickets = await mediator.Send(getTicketsByEventId, cancellationToken);
        return Ok(tickets);
    }

    /// <summary>
    /// Get ticket by TicketId
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{TicketId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid TicketId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var ticket = await mediator.Send(new GetTicketByIdQuery() { TicketId = TicketId }, cancellationToken);
        return Ok(ticket);
    }
    #endregion
}
