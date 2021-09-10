using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace oceny5._0.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext.User;

        public int? GetUserId => (int ?)int.Parse(User is null ? null : User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

    }
}
