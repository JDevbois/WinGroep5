using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.DataManagers
{
    //TODO not storing passwords in clear text
    //TODO subscriptions should be handled by a subscription manager
    public class UserManager
    {
        //TODO should be from db
        private static List<User> _users = new List<User>
            {
            //TODO subscriptions
                new User
                {
                    Id = 1,
                    Email = "Joren@heimdal.be",
                    Password = "aa",
                    Subscriptions = new CompanyManager().GetCompanies()
                },
                new User
                {
                    Id = 2,
                    Email = "miriam@heimdal.be",
                    Password = "bb"
                },
                new User
                {
                    Id = 3,
                    Email = "stephen@heimdal.be",
                    Password = "cc",
                    Subscriptions = new CompanyManager().GetCompanies()
                },
                new User
                {
                    Id = 4,
                    Email = "hans@heimdal.be",
                    Password = "dd",
                    Subscriptions = new CompanyManager().GetCompanies()
                }
            };

        public static User LoggedInUser { get; set; }

        //TODO
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User FindUser(
            string email,
            string password)
        {
            //TODO get data from database
            return GetUsers().First(u => u.Email == email && u.Password == password);
        }

        public List<Company> GetSubscriptionsForUser()
        {
            return LoggedInUser.Subscriptions;
        }

        private List<User> GetUsers()
        {
            return _users;
        }

        internal static bool IsUserLoggedIn()
        {
            return LoggedInUser != null;
        }

        public void LogOut()
        {
            LoggedInUser = null;
        }
    }
}
