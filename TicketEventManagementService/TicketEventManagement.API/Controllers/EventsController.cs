using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketEventManagement.Application.Handlers.Events.Commands.CreateEvent;
using TicketEventManagement.Application.Handlers.Events.Commands.UpdateEvent;
using TicketEventManagement.Application.Handlers.Events.Commands.DeleteEvent;
using TicketEventManagement.Application.Handlers.Events.Queries.GetListEvents;
using TicketEventManagement.Application.Handlers.Events.Queries.GetEvent;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Handlers.Events.Commands.PublishEvent;
using Microsoft.AspNetCore.Authorization;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Events management controller
/// </summary>
[Authorize]
[Route("Events")]
[ApiController]
public class EventsController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create event 
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateEventAsync(
        CreateEventCommand createEventCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newEvent = await mediator.Send(createEventCommand, cancellationToken);
        return Created("/Events/" + newEvent.EventId, newEvent);
    }

    /// <summary>
    /// Update event. If event is published, changes are sent to Search service
    /// </summary>
    [HttpPut("{eventId}")]
    public async Task<IActionResult> UpdateEventAsync(
        [FromRoute] Guid eventId,
        UpdateEventCommandDto dto,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        UpdateEventCommand updateEventCommand = new UpdateEventCommand()
        {
            EventId = eventId,
            Name = dto.Name,
            Description = dto.Description,
            UriMainImage = dto.UriMainImage,
            DateTimeEventStart = dto.DateTimeEventStart,
            DateTimeEventEnd = dto.DateTimeEventEnd,
            IsActive = dto.IsActive
            
        };
        var updatedEvent = await mediator.Send(updateEventCommand, cancellationToken);
        return Ok(updatedEvent);
    }

    /// <summary>
    /// Delete event. If event is published, changes are sent to Search service
    /// </summary>
    [HttpDelete("{eventId}")]
    public async Task<IActionResult> DeleteEventAsync(
        [FromRoute] Guid eventId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteEventCommand() { EventId = eventId }, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Publish event. Event with tickets sending to Search service
    /// </summary>
    [HttpPost("{eventId}")]
    public async Task<IActionResult> PublishEventAsync(
        [FromRoute] Guid eventId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new PublishEventCommand() { EventId = eventId }, cancellationToken);
        return Ok();
    }
    #endregion


    #region Queries
    /// <summary>
    /// Get list of events
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetListEventsQuery getListEventsQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var events = await mediator.Send(getListEventsQuery, cancellationToken);
        return Ok(events);
    }

    /// <summary>
    /// Get event by id
    /// </summary>
    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid eventId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var myEvent = await mediator.Send(new GetEventQuery() { EventId = eventId }, cancellationToken);
        return Ok(myEvent);
    }
    #endregion
}
