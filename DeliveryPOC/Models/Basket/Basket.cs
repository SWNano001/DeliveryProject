using System;
using System.Collections.Generic;

namespace DeliveryPOC.Models.Basket
{
    public class ProductItem
    {
        public int item_id { get; set; }
        public int item_qty { get; set; }
    }

    public class Basket
    {
        public int Basket_id { get; set; }
        public int User_id { get; set; }
        public string Item_id { get; set; }
        public string Item_unit { get; set; }
        public string Basket_status { get; set; }
        public List<ProductItem> Items { get; set; }
    }

    public class BasketList
    {
        public bool error { get; set; }
        public Basket data { get; set; }
        public string message { get; set; }
    }
}
