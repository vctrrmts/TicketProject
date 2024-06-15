using System.Security.Claims;
using TicketEventManagement.Application.Abstractions.Service;

namespace TicketEventManagement.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public int CurrentUserId => Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public string[] CurrentUserRoles => _contextAccessor.HttpContext.User.Claims
            .Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();

        string ICurrentUserService.AccessToken => _contextAccessor.HttpContext.Request.Headers["Authorization"]
            .ToString();
    }
}
