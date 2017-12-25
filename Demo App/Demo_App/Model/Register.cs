using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
    class Register
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class RequestData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PostCode { get; set; }
        public string Website { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string CreationDate { get; set; }
    }
    public class CreateAccount
    {
        public string Url { get; set; }
        public RequestData RequestData { get; set; }
    }
}
