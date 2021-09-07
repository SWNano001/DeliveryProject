using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DeliveryEmployeePOC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryEmployeePOC.Controllers
{
    public class UsersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(CallAPI());
        }


        private async Task<IEnumerable<UsersModel>> CallAPI()
        {
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                    //client.DefaultRequestHeaders.Add("", "");

                    var responseTask = client.GetAsync("/users");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var str = await result.Content.ReadAsStringAsync();
                        List<UsersModel> users = JsonConvert.DeserializeObject<List<UsersModel>>(str);

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

        [HttpPost]
        public async Task<JsonResult> AllUsers()
        {
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                    //client.DefaultRequestHeaders.Add("", "");

                    var responseTask = client.GetAsync("/users");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var str = await result.Content.ReadAsStringAsync();
                        List<UsersModel> users = JsonConvert.DeserializeObject<List<UsersModel>>(str);

                        return Json(new { Users = users });

                    }


                }

                return Json(new { Users = "" });
            }
            catch
            {
                return Json(new { Users = "" });
            }

        }


        [HttpPost]
        public JsonResult SendSMS()
        {
            using (var client = new HttpClient())
            {

                string token = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("2c8a5cdd88ac9ba7a13c5f125d22b69d:351d14ff4dafbbfe7694119ca57fced5"));

                var content = new Dictionary<string, string>();
                content.Add("msisdn", "0859734136");
                content.Add("message", "Order ของคุณเริ่มจัดส่งแล้ว...");
                content.Add("sender", "Demo");
                content.Add("force", "standard");


                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://api-v2.thaibulksms.com/sms"),
                    Headers = {
                        { HttpRequestHeader.Authorization.ToString(), "Basic "+token },
                        { HttpRequestHeader.Accept.ToString(), "application/json" }
                    },
                    //Content = new StringContent(JsonConvert.SerializeObject(svm))
                    Content = new FormUrlEncodedContent(content)
                };

                var response = client.SendAsync(httpRequestMessage).Result;


                if (response.IsSuccessStatusCode)
                {
                    //var x=await response.Content.ReadAsStringAsync();
                    return Json(new { massage="sucess"});
                }

                return Json(new { massage = "fail" });
            }
        }
    }
}
