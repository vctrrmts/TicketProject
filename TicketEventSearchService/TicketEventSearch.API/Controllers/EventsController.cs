using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;
using TicketEventSearch.Application.Handlers.Events.Commands.UpdateEvent;
using TicketEventSearch.Application.Handlers.Events.Queries.GetEventById;
using TicketEventSearch.Application.Handlers.Events.Queries.GetEvents;


namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Events search controller
/// </summary>
[Authorize]
[Route("Events")]
[ApiController]
public class EventsController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create event with tickets. The command is sent from management service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateEventAsync(
        CreateEventCommand createEventCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(createEventCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Update event. The command is sent from management service
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateEventAsync(
        UpdateEventCommand updateEventCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var updatedEvent = await mediator.Send(updateEventCommand, cancellationToken);
        return Ok(updatedEvent);
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of events
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetEventsQuery getListQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var events = await mediator.Send(getListQuery, cancellationToken);
        return Ok(events);
    }

    /// <summary>
    /// Get event by id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{EventId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid EventId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var eventt = await mediator.Send(new GetEventByIdQuery() { EventId = EventId }, cancellationToken);
        return Ok(eventt);
    }
    #endregion
}
