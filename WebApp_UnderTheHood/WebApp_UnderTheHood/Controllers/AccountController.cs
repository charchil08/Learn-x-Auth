using Microsoft.AspNetCore.Mvc;
using WebApp_UnderTheHood.Entities.ViewModels;
using WebApp_UnderTheHood.Entities.Helper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WebApp_UnderTheHood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginVM request)
        {
            if (!ModelState.IsValid) return View(request);
            if (request.UserName.Equals(Constants.UserName) && request.Password.Equals(Constants.Password))
            {
                //Generate Claims
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Name, request.UserName),
                    new Claim(ClaimTypes.Email, Constants.Email),
                    new Claim(Constants.JoinedDateClaimKey, "2023-01-01")
            };

                //Generate Identity context with claims
                var identity = new ClaimsIdentity(claims, Constants.TokenScheme);

                //Create Claims Prinicipal from this
                ClaimsPrincipal principal = new(identity);

                //For Persistent cookie setting
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    IsPersistent = request.RememberMe,
                };

                //Encrypt cookie and signin process flow
                await HttpContext.SignInAsync(Constants.TokenScheme, principal, properties);

                return RedirectToAction("Index", "Home");
            }
            return View(request);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
