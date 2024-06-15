using AutoMapper;
using MediatR;
using UsersManagement.Application.Abstractions.Persistence;
using UsersManagement.Application.DTOs;
using UsersManagement.Domain;

namespace UsersManagement.Application.Handlers.Query.GetList;

public class GetListQueryHandler : IRequestHandler<GetListQuery, IReadOnlyCollection<GetUserDto>>
{
    private readonly IBaseRepository<User> _users;
    //private readonly MemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public GetListQueryHandler(IBaseRepository<User> users, /*UsersMemoryCache memoryCache,*/ IMapper mapper)
    {
        _users = users;
        //_memoryCache = memoryCache.Cache;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<GetUserDto>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        //var cacheKey = JsonSerializer.Serialize($"GetList:{request}", new JsonSerializerOptions
        //{
        //    ReferenceHandler = ReferenceHandler.IgnoreCycles
        //});

        //if (_memoryCache.TryGetValue(cacheKey, out IReadOnlyCollection<GetUserDto>? result))
        //{
        //    return result!;
        //}

        var result = _mapper.Map<IReadOnlyCollection<GetUserDto>>(await _users.GetListAsync(
        request.Offset,
        request.Limit,
        request.LoginFreeText == null ? null : x => x.Login.Contains(request.LoginFreeText),
        x => x.UserId, false,
        cancellationToken));

        //var cacheEntryOptions = new MemoryCacheEntryOptions()
        //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
        //    .SetSlidingExpiration(TimeSpan.FromSeconds(5))
        //    .SetSize(3);

        //_memoryCache.Set(cacheKey, result, cacheEntryOptions);

        return result;
    }
}
