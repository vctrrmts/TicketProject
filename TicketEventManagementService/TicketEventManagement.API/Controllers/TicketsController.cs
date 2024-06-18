using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventManagement.Application.Handlers.Tickets.Commands.CreateTickets;
using TicketEventManagement.Application.Handlers.Tickets.Commands.DeleteTicket;
using TicketEventManagement.Application.Handlers.Tickets.Commands.UpdateTicketsPrice;
using TicketEventManagement.Application.Handlers.Tickets.Queries.GetListTickets;
using TicketEventManagement.Application.Handlers.Tickets.Queries.GetTicket;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Tickets management controller
/// </summary>
[Authorize]
[Route("Manage/Tickets")]
[ApiController]
public class TicketsController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create tickets of event according to the seating scheme. Can set price for all tickets
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTicketAsync(
        CreateTicketsCommand createTicketsCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newTickets = await mediator.Send(createTicketsCommand, cancellationToken);
        return Ok(newTickets);
    }

    /// <summary>
    /// Update price of tickets by seats range according to the seating scheme
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateTicketsPriceAsync(
        UpdateTicketsPriceCommand updateTicketsPriceCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var updatedTickets = await mediator.Send(updateTicketsPriceCommand, cancellationToken);
        return Ok(updatedTickets);
    }

    /// <summary>
    /// Delete all event tickets
    /// </summary>
    /// <param name="deleteTicketsCommand"></param>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(
        DeleteTicketsCommand deleteTicketsCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(deleteTicketsCommand, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of event tickets
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetListTicketsQuery getListTicketsQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var tickets = await mediator.Send(getListTicketsQuery, cancellationToken);
        return Ok(tickets);
    }

    /// <summary>
    /// Get ticket by ticketId
    /// </summary>
    [HttpGet("{ticketId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid ticketId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var ticket = await mediator.Send(new GetTicketQuery() { TicketId = ticketId }, cancellationToken);
        return Ok(ticket);
    }
    #endregion
}
