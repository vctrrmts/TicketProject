using AutoMapper;
using MediatR;
using Serilog;
using System.Runtime.CompilerServices;
using System.Text.Json;
using TicketEventManagement.Application.Abstractions.Persistence;
using TicketEventManagement.Application.Abstractions.Service;
using TicketEventManagement.Application.DTOs;
using TicketEventManagement.Application.ExternalRepositories;
using TicketEventManagement.Domain;

namespace TicketEventManagement.Application.Handlers.Categories.Commands.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, GetCategoryDto>
{
    private readonly IBaseRepository<Category> _categories;

    private readonly ICategoryRepository _categoryRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public CreateCategoryCommandHandler(IBaseRepository<Category> categories, IMapper mapper,
        ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
    {
        _categories = categories;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<GetCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category(request.Name);
        newCategory = await _categories.AddAsync(newCategory, cancellationToken);

        string token = _currentUserService.AccessToken;
        await _categoryRepository.CreateCategoryAsync(newCategory, token,cancellationToken);
        Log.Information("Category added " + JsonSerializer.Serialize(request));

        return _mapper.Map<GetCategoryDto>(newCategory);
    }
}
