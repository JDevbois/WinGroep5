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
        public ObservableCollection<Company> Subscriptions { get; set; }

        public User(string email)
        {
            this.Email = email;
        }
    }
}