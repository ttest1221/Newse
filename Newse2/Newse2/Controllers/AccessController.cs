using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newse2.Models;
using Microsoft.EntityFrameworkCore;

namespace Newse2.Controllers
{
    public class AccessController : Controller
    {
        private readonly NewseDBContext _context;
        public IActionResult Register()
        {
            return View(NewseDBContext.GetContext().Users.ToList());
        }
        public IActionResult Login()
        {

            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { username = claimUser.Claims.ToList()[0].Value });
            }
               


            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelLogin)
        {
            try
            {
                string nickname = NewseDBContext.GetContext().Users.Where(x => x.Login == modelLogin.Login).First().Nickname;
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, nickname)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = modelLogin.RememberMe
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home", new { profileLogin = claims.First().Value });
            }
            catch (Exception ex)
            {
                ViewData["ValidateMessage"] = "user not found";
                return RedirectToAction("Login", "Access");
            }
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
