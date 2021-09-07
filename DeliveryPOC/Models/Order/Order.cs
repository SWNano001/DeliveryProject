using System;
namespace DeliveryPOC.Models.Order
{
    public class Order
    {
        public int PageOrder_id { get; set; }
        public int Basket_id { get; set; }
        public int Location_id { get; set; }
        public int Payment_id { get; set; }
        public int User_id { get; set; }
        public string PageOrder_status { get; set; }
    }

    public class OrderList
    {
        public bool error { get; set; }
        public Order data { get; set; }
        public string message { get; set; }
    }
}
