namespace SignalRExample.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "test"));
            identity.AddClaim(new Claim(ClaimTypes.Name, "test"));
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                properties);

            return RedirectToAction(string.Empty, string.Empty);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(string.Empty, string.Empty);
        }
    }
}
