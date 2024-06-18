using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventSearch.Application.Handlers.Categories.Commands.CreateCategory;
using TicketEventSearch.Application.Handlers.Categories.Commands.DeleteCategory;
using TicketEventSearch.Application.Handlers.Categories.Commands.UpdateCategory;
using TicketEventSearch.Application.Handlers.Categories.Queries.GetCategory;
using TicketEventSearch.Application.Handlers.Categories.Queries.GetListCategories;

namespace TicketEventSearch.API.Controllers;

/// <summary>
/// Search categories controller
/// </summary>
[Authorize]
[Route("Search/Categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    # region Commands
    /// <summary>
    /// Create category. The command is sent from management service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(
    CreateCategoryCommand createCategoryCommand,
    IMediator mediator,
    CancellationToken cancellationToken)
    {
        await mediator.Send(createCategoryCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Update category. The command is sent from management service
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateCategoryAsync(
        UpdateCategoryCommand updateCategoryCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(updateCategoryCommand, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Delete category. The command is sent from management service
    /// </summary>
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategoryAsync(
        [FromRoute] Guid categoryId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCategoryCommand() { CategoryId = categoryId }, cancellationToken);
        return Ok();
    }
    #endregion

    #region Queries
    /// <summary>
    /// Get list of categories
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
    [FromQuery] GetListCategoriesQuery getListCategoriesQuery,
    IMediator mediator,
    CancellationToken cancellationToken)
    {
        var categories = await mediator.Send(getListCategoriesQuery, cancellationToken);
        return Ok(categories);
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByIdAsync(
        Guid categoryId,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery() { CategoryById = categoryId }, cancellationToken);
        return Ok(category);
    }
    #endregion
}
