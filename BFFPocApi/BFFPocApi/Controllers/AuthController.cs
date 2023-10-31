using BFFPocApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BFFPocApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("APIAllowOrigins")]
    public class AuthController : ControllerBase
    {
        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            var claimsPrinciple = CookieCreationService.CreateCookie();

            await HttpContext.SignInAsync("Cookies", claimsPrinciple, new AuthenticationProperties
            {
                IsPersistent= true,
                AllowRefresh=true,
                ExpiresUtc= DateTime.UtcNow.AddMinutes(10)
            });

            return Ok();
        }

        [HttpGet("CookieCheck")]
        [Authorize(AuthenticationSchemes = "Cookies")]
        public IActionResult CookieCheck()
        {
            return Ok("ECookie Valid");
        }
    }
}
