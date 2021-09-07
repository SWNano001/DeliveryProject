using System;
using System.Collections.Generic;

namespace DeliveryPOC.Models.Product
{
    public class Product
    {
        public int Item_id { get; set; }
        public string Item_name { get; set; }
        public int Item_price { get; set; }
        public int Item_stock { get; set; }
        public string Item_des { get; set; }
        public int Category_id { get; set; }
    }

    public class ProductList
    {
        public bool error { get; set; }
        public List<Product> data { get; set; }
        public string message { get; set; }
    }

    public class OneProduct
    {
        public bool error { get; set; }
        public Product data { get; set; }
        public string message { get; set; }
    }


}
