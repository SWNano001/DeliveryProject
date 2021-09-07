using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Threading.Tasks;
using DeliveryPOC.Models;
using DeliveryPOC.Models.Basket;
using DeliveryPOC.Models.Location;
using DeliveryPOC.Models.Login;
using DeliveryPOC.Models.Order;
using DeliveryPOC.Models.Product;
using DeliveryPOC.Models.RoboShipAPIConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryPOC.Controllers
{
    public class RoboShipController : Controller
    {
        private readonly IOptions<RoboShipAPI> _roboShipAPIConig;
       

        public RoboShipController(IOptions<RoboShipAPI> roboShipAPI)
        {
            _roboShipAPIConig = roboShipAPI;
           
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //Get Location
        [HttpPost]
        public async Task<JsonResult> LocationList()
        {
            try
            {
                using(var client=new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL+"/locations"),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        }
                    };

                    var response = client.SendAsync(httpRequestMessage).Result;

                    if (response.IsSuccessStatusCode)
                    {
                       
                        var data = await response.Content.ReadAsStringAsync();
                       LocationList locationlist = JsonConvert.DeserializeObject<LocationList>(data);


                        return Json(new { locationlist = locationlist.data });
                    }

                    return Json(new { locationlist = "" });
                }
            }

            catch(Exception ex)
            {
                return Json(new { locationlist = ex.Message });
            }
        }


       [HttpPost]
       public async Task<JsonResult> AllProduct()
        {
            try
            {

                using (var client = new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/items"),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        }
                    };


                    var response = client.SendAsync(httpRequestMessage).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        ProductList productlist = JsonConvert.DeserializeObject<ProductList>(data);


                        return Json(new { productlist = productlist.data });
                    }

                    return Json(new { locationlist = "" });
                }


            }
            catch (Exception ex)
            {
                return Json(new { locationlist = ex.Message });
            }
        }


        [HttpPost]
       public async Task<JsonResult> Product(int id)
        {
            try
            {
                using(var client =new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/item/"+id),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        }
                    };
                    var response = client.SendAsync(httpRequestMessage).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        OneProduct productlist = JsonConvert.DeserializeObject<OneProduct>(data);


                        return Json(new { locationlist = productlist.data });
                    }

                    return Json(new { locationlist = "" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { locationlist = ex.Message });
            }
        }



        public async Task<User> Login(string phone,string password)
        {
            try
            {
                using(var client=new HttpClient())
                {
                    var content = new Dictionary<string, string>();
                    content.Add("phone", phone);
                    content.Add("password", password);
                    

                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/loginUser"),
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


        [HttpPost]
        public async Task<JsonResult> GetOrderByID()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/order/1"),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        }
                    };

                    var response = client.SendAsync(httpRequestMessage).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        OrderList orderlist = JsonConvert.DeserializeObject<OrderList>(data);


                        return Json(new { orderlist = orderlist.data });
                    }

                    return Json(new { orderlist = "" });
                }
            }
            catch
            {
                return Json(new { orderlist = "" });
            }
        }


        [HttpPost]
        public async Task<JsonResult> GetBasket()
        {
            try
            {
                using(var client=new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(_roboShipAPIConig.Value.BaseURL + "/basket/1"),
                        Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" }
                        }
                    };

                    var response = client.SendAsync(httpRequestMessage).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStringAsync();
                        BasketList basketlist = JsonConvert.DeserializeObject<BasketList>(data);


                        return Json(new { basketlist = basketlist.data });
                    }

                    return Json(new { basketlist = "" });

                }
            }
            catch
            {
                return Json(new { basketlist = "" });
            }
        }


    }
}
