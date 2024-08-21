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
                        //var response = new LoginResponseDto
                        //{
                        //    JwtToken = jwtToken,
                        //};

                        //return Ok(response);

                    }
                }
            }
            //    if (user == null)
            //{
            //    TempData["Alert"] = "danger|Username or password incorrect, please try again.";
            //    return Redirect("Index");                
            //} else
            //{
            //    // Create and store session
            //    string guid = null;

            //    Session session = _db.Sessions.FirstOrDefault(session => session.UserId == user.Id);

            //    // Gives user the same session back if a session is already detected but updates timestamp
            //    if (session == null)
            //    {
            //        guid = Guid.NewGuid().ToString();
            //        _db.Sessions.Add(new Session
            //        {
            //            Id = guid,
            //            UserId = user.Id,
            //            TimeStamp = DateTime.Now
            //        });
            //    } else
            //    {
            //        guid = session.Id;
            //        session.TimeStamp = DateTime.Now;
            //    }

            //    _db.SaveChanges();

            //    Response.Cookies.Append("sessionId", guid, new CookieOptions
            //    {
            //        HttpOnly = true,
            //        SameSite = SameSiteMode.Lax
            //    });

            //    TempData["Alert"] = "primary|Successfully logged in!";

            //    foreach (var cart in _db.Carts.Where(cart => cart.UserId == user.Id))
            //    {
            //        _db.Carts.Remove(cart);
            //    }

            //    string cartCookie = HttpContext.Request.Cookies["guestCart"];                
            //    if (cartCookie != null)
            //    {
            //        // Overwrites current cart in account with guestCart if guestCart exists as per CW's specifications
            //        GuestCart guestCart = JsonSerializer.Deserialize<GuestCart>(cartCookie);
            //        foreach (var product in guestCart.Products)
            //        {
            //            _db.Carts.Add(new Cart
            //            {
            //                ProductId = product.ProductId,
            //                UserId = user.Id,
            //                Quantity = product.Quantity
            //            });
            //        }
            //        HttpContext.Response.Cookies.Delete("guestCart");
            //        TempData["Alert"] += $" {guestCart.Count()} item(s) from your previous cart has overwritten your account cart.";
            //    }
            //    _db.SaveChanges();
            //}

            // TempData was not expiring fast enough in some use cases, this ensures removal after single usage
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
