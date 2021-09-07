using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeliveryPOC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DeliveryPOC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        [Authorize(Roles ="Admin,User")]
        public IActionResult Index()
        {
            var user = HttpContext.User.Claims.FirstOrDefault();
            if (Request.Cookies["AccountName"] !=null)
            {
                ViewBag.AccountName = Request.Cookies["AccountName"];
            }
            else
            {
                var x = user.Subject.Claims.ToList(); 
                ViewBag.AccountName =x[2].Value;
            }
           
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
