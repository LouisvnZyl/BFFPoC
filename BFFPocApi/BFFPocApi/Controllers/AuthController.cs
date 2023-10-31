using BFFPocApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BFFPocApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            var claimsPrinciple = CookieCreationService.CreateCookie();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple, new AuthenticationProperties
            {
                IsPersistent= true,
                AllowRefresh=true,
                ExpiresUtc= DateTime.UtcNow.AddMinutes(10)
            });

            return Ok("Signed in successfully");
        }
    }
}
