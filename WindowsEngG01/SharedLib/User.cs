using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib
{
    public class User : BasePerson
    {
        public List<String> Subscriptions { get; set; }
        public List<Company> Companies { get; set; }

        public User() : this(new List<Company>(), new List<String>())
        {
        }

        public User(List<Company> companies) : this(companies, new List<String>())
        {
            Companies = companies;
        }

        public User(List<Company> companies, List<String> subscriptions)
        {
            Companies = companies;
            Subscriptions = subscriptions;
        }
    }
}