using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DeliveryEmployeePOC.Models.Login;
using DeliveryEmployeePOC.Models.RoboshipAPIConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryEmployeePOC.Controllers
{
    public class RoboShipAPIController : Controller
    {
        private readonly IOptions<RoboShipAPI> _roboShipAPIConig;

        public RoboShipAPIController(IOptions<RoboShipAPI> roboShipAPI)
        {
            _roboShipAPIConig = roboShipAPI;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<UserLogin> Login(string phone, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new Dictionary<string, string>();
                    content.Add("phone", phone);
                    content.Add("password", password);


                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/loginAdmin"),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        },
                        Content = new FormUrlEncodedContent(content)
                    };

                    var response = client.SendAsync(httpRequestMessage).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        Login login = JsonConvert.DeserializeObject<Login>(data);


                        return login.data;
                    }

                    return null;

                }
            }
            catch
            {
                return null;
            }
        }

        //ดึงข้อมูล Order
        [HttpPost]
        public void GetOrderList()
        {
            try
            {
                using(var client=new HttpClient())
                {

                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/items"),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        }
                    };


                }
            }
            catch
            {

            }
        }

    }
}
