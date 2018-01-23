using System;
using System.Collections.Generic;
using System.Text;

namespace Demo_App.Model
{
   public class Customer
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address  { get; set; }

        public string PostCode { get; set; }

        public string Email { get; set; }

        public string TelephoneNo { get; set; }

        public string CreationDate { get; set; }

        public int EntityStatus { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public string StreetName { get; set; }

        public string CityName { get; set; }

        public int ZipCode { get; set; }

        public string CreationDate { get; set; }
    }
}
