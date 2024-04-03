using Dev.Business.Models.Base;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Dev.Api.Extensions.IdentityUser
{
    public class AppIdentityUser : IAppIdentityUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AppIdentityUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public Guid GetUserId()
        {
            if(!IsAuthenticated()) return Guid.Empty;

            var claim = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claim))
            {
                claim = _accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            }

            return claim is null ? Guid.Empty : Guid.Parse(claim);
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
        public string GetLocalIpAddress()
        {
            throw new NotImplementedException();
        }

        public string GetRemoteIpAddress()
        {
            throw new NotImplementedException();
        }
    }
}
