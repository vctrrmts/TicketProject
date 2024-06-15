using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Handlers.Commands.CreateEvent;
using Notification.Application.Handlers.Queries.GetEvents;

namespace Notification.API.Controllers;

/// <summary>
/// Notification events controller
/// </summary>
[Authorize]
[Route("Events")]
[ApiController]
public class EventsController : ControllerBase
{
    /// <summary>
    /// Add event for which notifications were sent
    /// </summary>
    #region Commands
    [HttpPost]
    public async Task<IActionResult> CreateEventAsync(
        CreateEventCommand createEventCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(createEventCommand, cancellationToken);
        return Ok();
    }
    #endregion

    /// <summary>
    /// Get events for which notifications were sent
    /// </summary>
    #region Queries
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetEventsQuery getEventsQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var events = await mediator.Send(getEventsQuery, cancellationToken);
        return Ok(events);
    }
    #endregion
}