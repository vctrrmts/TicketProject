using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketControlService.Application.Handlers.Commands.VerifyTicket;

namespace TicketControlService.API.Controllers;

/// <summary>
/// Ticket Control Controller
/// </summary>
[Authorize]
[Route("TicketControl")]
[ApiController]
public class TicketController : ControllerBase
{
    /// <summary>
    /// Checks whether the ticket belongs to the event and whether it has already passed
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> VerifyTicket(
        VerifyTicketCommand verifyTicketCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(verifyTicketCommand, cancellationToken);
        return Ok(result);
    }
}