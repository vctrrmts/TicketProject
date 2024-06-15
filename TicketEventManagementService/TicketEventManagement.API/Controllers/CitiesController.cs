using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Handlers.Cities.Commands.CreateCity;
using TicketEventManagement.Application.Handlers.Cities.Commands.DeleteCity;
using TicketEventManagement.Application.Handlers.Cities.Commands.UpdateCity;
using TicketEventManagement.Application.Handlers.Cities.Queries.GetCity;
using TicketEventManagement.Application.Handlers.Cities.Queries.GetListCity;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Cities management controller
/// </summary>
[Authorize]
[Route("Cities")]
[ApiController]
public class CitiesController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create city with sending command to Search Service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCityAsync(
        CreateCityCommand createCityCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newCity = await mediator.Send(createCityCommand, cancellationToken);
        return Created("/Cities/" + newCity.CityId, newCity);
    }

    /// <summary>
    /// Update city with sending command to Search Service
    /// </summary>
    [HttpPut("{cityId}")]
    public async Task<IActionResult> UpdateTicketAsync(
        [FromRoute] Guid cityId,
        UpdateCityCommandDto dto,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        UpdateCityCommand updateCityCommand = new UpdateCityCommand()
        {
            CityId = cityId,
            Name = dto.Name,
            IsActive = dto.IsActive,
        };
        var updatedCity = await mediator.Send(updateCityCommand, cancellationToken);
        return Ok(updatedCity);
    }

    /// <summary>
    /// Delete city with sending command to Search Service
    /// </summary>
    [HttpDelete("{cityId}")]
    public async Task<IActionResult> DeleteAsync(
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
    [HttpGet("{cityId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid cityId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var city = await mediator.Send(new GetCityByIdQuery() { CityId = cityId }, cancellationToken);
        return Ok(city);
    }
    #endregion
}
