using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeliveryEmployeePOC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace DeliveryEmployeePOC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Index()
        {
            
            ViewBag.Username = Request.Cookies["Username"];
            //ViewBag.RESTAPI = CallAPI();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult AssignTask()
        {
            return View();
        }

        


        private async Task<IEnumerable<UsersModel>> CallAPI()
        {
            try
            {
                

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                    client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                    var responseTask = client.GetAsync("/users");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var resultcontent=await result.Content.ReadAsStringAsync();
                        List<UsersModel> users = JsonConvert.DeserializeObject<List<UsersModel>>(resultcontent);

                        return users;

                    }
                    

                }
                
                return null;
            }
            catch
            {
                return null;
            }
            
        }






    }


}
