using AutoMapper;
using MediatR;
using Serilog;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.Exceptions;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Categories.Commands.UpdateCategory;

internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, GetCategoryDto>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly ICategoryRepository _categoryRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public UpdateCategoryCommandHandler(IBaseRepository<Category> categories,
        ICategoryRepository categoryRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _categories = categories;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task<GetCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categories.SingleOrDefaultAsync(x => x.CategoryId == request.CategoryId, cancellationToken);
        if (category == null)
        {
            throw new NotFoundException($"Category with CategoryId = {request.CategoryId} not found");
        }

        category.UpdateName(request.Name);
        category.UpdateIsActive(request.IsActive);
        await _categories.UpdateAsync(category, cancellationToken);

        string token = _currentUserService.AccessToken;
        await _categoryRepository.UpdateCategoryAsync(category, token, cancellationToken);
        Log.Information("Category updated " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetCategoryDto>(category);
    }
}
