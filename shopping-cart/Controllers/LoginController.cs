using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SA51_CA_Project_Team10.DBs;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using SA51_CA_Project_Team10.Models.Domain;
using SA51_CA_Project_Team10.Repositories;
using Microsoft.AspNetCore.Http;

namespace SA51_CA_Project_Team10.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;
        public LoginController( UserManager<User> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        public IActionResult Index(Verify v)
        {
            //string sessionId = HttpContext.Request.Cookies["sessionId"];
            //if (v.VerifySession(sessionId, _db))
            //{
            //    TempData["Alert"] = "primary|Already logged in!";
            //    return Redirect("/Gallery/Index");
            //}
            //else {
            //    string cartCookie = HttpContext.Request.Cookies["guestCart"];
            //    if (cartCookie != null)
            //    {
            //        GuestCart guestCart = JsonSerializer.Deserialize<GuestCart>(cartCookie);
            //        ViewData["CartQuantity"] = guestCart.Count();
            //    }
            //    else
            //    {
            //        ViewData["CartQuantity"] = 0;
            //    }
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string username, string password, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                        Response.Cookies.Append("JwtToken", jwtToken, new CookieOptions
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.Lax,
                            Secure = true, 
                            Expires = DateTime.UtcNow.AddDays(7) 
                        });
                        HttpContext.Response.Cookies.Delete("sessionId");
                        HttpContext.Response.Cookies.Delete("guestCart");
                        if (TempData["ReturnUrl"] != null)
                        {
                            TempData.Remove("ReturnUrl");
                        }
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("/Gallery/Index");
                    }
                }
            }

            if (TempData["ReturnUrl"] != null)
            {
                TempData.Remove("ReturnUrl");
            }

            if (!returnUrl.IsNullOrEmpty())
            {
                return Redirect(returnUrl);
            }

            return Redirect("/Gallery/Index");
        }
    }
}
