using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using DeliveryPOC.Models.RoboShipAPIConfig;
using DeliveryPOC.Models.Login;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryPOC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOptions<RoboShipAPI> _roboShipAPIConig;
        public AccountController(IOptions<RoboShipAPI> roboShipAPI)
        {
            _roboShipAPIConig = roboShipAPI;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
           
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username,string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login","Account");
            }
            ClaimsIdentity identity = null;
            bool isAuthenthicate = false;

            RoboShipController roboship = new RoboShipController(_roboShipAPIConig);

            User user =await roboship.Login(username, password);

            if(user!=null)
            {
                identity=new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim(ClaimTypes.Name, user.User_phone_num));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.User_status));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.User_fname+" "+user.User_lname));
                identity.AddClaim(new Claim(ClaimTypes.Sid, user.User_id.ToString()));
                isAuthenthicate = true;
            }

           /* if(username=="demo" && password == "P@ssw0rd1234")
            {
                identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.Name, username));
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                isAuthenthicate = true;
            }*/

            if (isAuthenthicate == true)
            {
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                    IsPersistent = true,
                };
                var login=HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);

                

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login","Account");
        }


       
    }
}
