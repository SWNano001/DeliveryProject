using System;
namespace DeliveryPOC.Models.Login
{
    public class User
    {
        public int User_id { get; set; }
        public string User_phone_num { get; set; }
        public string User_password { get; set; }
        public string User_fname { get; set; }
        public string User_lname { get; set; }
        public string User_status { get; set; }
    }

    public class Login
    {
        public bool error { get; set; }
        public User data { get; set; }
        public string message { get; set; }
    }
}
