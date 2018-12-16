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
        public List<Company> Subscriptions { get; set; }
        public List<Company> Companies { get; set; }

        public User() : this(new List<Company>(), new List<Company>())
        {
        }

        public User(List<Company> companies) : this(companies, new List<Company>())
        {
            Companies = companies;
        }

        public User(List<Company> companies, List<Company> subscriptions)
        {
            Companies = companies;
            Subscriptions = subscriptions;
        }
    }
}