using BFFPocApi.AuthorizationRequirements;
using Microsoft.AspNetCore.Authorization;

namespace BFFPocApi.AuthorizationHandlers
{
    public class CookieAuthorizationHandler : AuthorizationHandler<CookieAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieAuthorizationHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor= contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CookieAuthorizationRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
