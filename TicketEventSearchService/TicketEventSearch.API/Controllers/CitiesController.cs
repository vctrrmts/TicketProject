using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.Handlers.Cities.Commands.CreateCity;
using TicketEventSearch.Application.Handlers.Cities.Commands.DeleteCity;
using TicketEventSearch.Application.Handlers.Cities.Commands.UpdateCity;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetCity;
using TicketEventSearch.Application.Handlers.Cities.Queries.GetListCity;

namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Cities Search controller
/// </summary>
[Authorize]
[Route("Search/Cities")]
[ApiController]
public class CitiesController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create city. The command is sent from management service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCityAsync(
        CreateCityCommand createCityCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(createCityCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Update city. The command is sent from management service
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateCityAsync(
        UpdateCityCommand updateCityCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(updateCityCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Delete city. The command is sent from management service
    /// </summary>
    [HttpDelete("{cityId}")]
    public async Task<IActionResult> DeleteCityAsync(
        [FromRoute] Guid cityId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCityCommand() { CityId = cityId }, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of cities
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] GetCityListQuery getCityListQuery,
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var cities = await mediator.Send(getCityListQuery, cancellationToken);
        return Ok(cities);
    }

    /// <summary>
    /// Get city by id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{cityId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid cityId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var eventt = await mediator.Send(new GetCityByIdQuery() { CityId = cityId }, cancellationToken);
        return Ok(eventt);
    }
    #endregion
}
