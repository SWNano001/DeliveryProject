using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryPOC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryPOC.Controllers
{
    public class CartController : Controller
    {

        private readonly IOptions<MyConfig> config;
        public CartController(IOptions<MyConfig> config)
        {
            this.config = config;
           
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            //var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MyConfig")["ApplicationName"];

            //ViewBag.Test =config.Value.ApplicationName;


            //Response.Cookies.Append("Test", "sathaporn");

            /*CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("Test", "sathaporn.won", options);*/

            ViewBag.Test = "Test";
            return View();
        }
    }
}
