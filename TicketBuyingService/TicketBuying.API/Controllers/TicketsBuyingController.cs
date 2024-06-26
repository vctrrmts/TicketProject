﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBuying.Application.Handlers.Commands.BuyTicket;
using TicketBuying.Application.Handlers.Commands.VerifyTicketCommand;
using TicketBuying.Application.Handlers.Queries.GetTicketsByEventId;

namespace TicketBuying.API.Controllers
{
    /// <summary>
    /// Ticket buying controller
    /// </summary>
    [Authorize]
    [Route("Buy/Tickets")]
    [ApiController]
    public class TicketsBuyingController : ControllerBase
    {
        /// <summary>
        /// Buy ticket with changing ticket's status in SearchService
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuyTicketAsync(
            [FromQuery] string mail,
            [FromBody] Guid[] tickets,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var buyTicketCommand = new BuyTicketCommand() 
            {
                TicketIds = tickets,
                Mail = mail
            };
            await mediator.Send(buyTicketCommand, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Get event tickets for sending notifications in NotiService
        /// </summary>
        [HttpGet("EventId/{eventId}/Tickets")]
        public async Task<IActionResult> GetListAsync(
            Guid eventId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var tickets = await mediator.Send(new GetTicketsByEventIdQuery() { EventId = eventId }, cancellationToken);
            return Ok(tickets);
        }

        /// <summary>
        /// Get ticket information by HashGuid for verification in ControlService
        /// </summary>
        [AllowAnonymous]
        [HttpPost("Verify")]
        public async Task<IActionResult> GetTicketByHash(
            VerifyTicketCommand verifyTicketCommand,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var ticket = await mediator.Send(verifyTicketCommand, cancellationToken);
            return Ok(ticket);
        }
    }
}
