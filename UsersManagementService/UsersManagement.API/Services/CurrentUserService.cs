using System.Security.Claims;
using UsersManagement.Application.Abstractions.Service;

namespace UsersManagement.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid CurrentUserId => Guid.Parse(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public string[] CurrentUserRoles => _contextAccessor.HttpContext.User.Claims
            .Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
    }
}
