using System.Security.Claims;
using TicketControlService.Application.Abstractions.Service;

namespace TicketControlService.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public int CurrentUserId => Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        string ICurrentUserService.AccessToken => _contextAccessor.HttpContext.Request.Headers["Authorization"]
            .ToString();
    }
}
