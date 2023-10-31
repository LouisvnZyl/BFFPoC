using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BFFPocApi.Services
{
    public static class CookieCreationService
    {
        public static ClaimsPrincipal CreateCookie()
        {
            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: "Test@test.com"),
                new Claim(type: ClaimTypes.Name, value: "TestCookieName")
            };

            var identity = new ClaimsIdentity(claims, "Cookies");

            return new ClaimsPrincipal(identity);
        }
    }
}
