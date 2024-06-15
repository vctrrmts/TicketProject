﻿using System.Security.Claims;
using Notification.Application.Abstractions.Service;

namespace Notification.API.Services
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
    }
}
