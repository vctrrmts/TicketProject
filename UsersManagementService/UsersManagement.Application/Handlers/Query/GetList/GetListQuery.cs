using MediatR;
using UsersManagement.Application.DTOs;

namespace UsersManagement.Application.Handlers.Query.GetList;

public class GetListQuery : IRequest<IReadOnlyCollection<GetUserDto>>
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? LoginFreeText { get; set; }
}
