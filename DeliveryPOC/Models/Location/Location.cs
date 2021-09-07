using System;
using System.Collections.Generic;

namespace DeliveryPOC.Models.Location
{
    public class Location
    {
        public int? Location_id { get; set; }
        public string Location_name { get; set; }
        public string Location_latitude { get; set; }
        public string Location_longitude { get; set; }
        public string Location_xyz { get; set; }
    }

    public class LocationList
    {
        public string error { get; set; }
        public List<Location> data { get; set; }
        public string message { get; set; }
    }

}
