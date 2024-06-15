using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Handlers.Categories.Commands.CreateCategory;
using TicketEventManagement.Application.Handlers.Categories.Commands.DeleteCategory;
using TicketEventManagement.Application.Handlers.Categories.Commands.UpdateCategory;
using TicketEventManagement.Application.Handlers.Categories.Queries.GetCategory;
using TicketEventManagement.Application.Handlers.Categories.Queries.GetListCategories;

namespace TicketEventManagement.API.Controllers;

/// <summary>
/// Categories of events management controller
/// </summary>
[Authorize]
[Route("Categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    #region Commands
    /// <summary>
    /// Create category with sending command to Search Service
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(
        CreateCategoryCommand createCategoryCommand,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var newCategory = await mediator.Send(createCategoryCommand, cancellationToken);
        return Created("/Categories/" + newCategory.CategoryId, newCategory);
    }

    /// <summary>
    /// Update category with sending command to Search Service
    /// </summary>
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategoryAsync(
        [FromRoute] Guid categoryId,
        UpdateCategoryCommandDto dto,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        UpdateCategoryCommand updateCategoryCommand = new UpdateCategoryCommand()
        {
            CategoryId = categoryId,
            Name = dto.Name,
            IsActive = dto.IsActive,
        };
        var updatedCategory = await mediator.Send(updateCategoryCommand, cancellationToken);
        return Ok(updatedCategory);
    }

    /// <summary>
    /// Delete category with sending command to Search Service
    /// </summary>
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteAsync(
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
