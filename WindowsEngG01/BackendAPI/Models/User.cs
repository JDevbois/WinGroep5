using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendAPI.Models
{
    public class User
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String City { get; set; }
        public String PostalCode { get; set; }
        public String Street { get; set; }
        public String Number { get; set; }
        public String Password { get; set; }

        public List<String> Subscriptions { get; set; }
        public List<Company> Companies { get; set; }
    }
}