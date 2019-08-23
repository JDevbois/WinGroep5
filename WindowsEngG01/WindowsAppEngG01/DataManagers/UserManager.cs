using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.Utils;

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
                    Id = "1",
                    Email = "Joren@heimdal.be",
                    Password = "aa",
                    Subscriptions = new CompanyManager().GetCompanies().Select(c => c.Id).ToList()
                },
                new User
                {
                    Id = "2",
                    Email = "miriam@heimdal.be",
                    Password = "bb"
                },
                new User
                {
                    Id = "3",
                    Email = "stephen@heimdal.be",
                    Password = "cc",
                    Subscriptions = new CompanyManager().GetCompanies().Select(c => c.Id).ToList()
                },
                new User
                {
                    Id = "4",
                    Email = "hans@heimdal.be",
                    Password = "dd",
                    Subscriptions = new CompanyManager().GetCompanies().Select(c => c.Id).ToList()
                }
            };

        public static User LoggedInUser { get; set; }

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

        public static User FindUser(string id)
        {
            return new UserManager().GetUsers().First(u => u.Id.Equals(id));
        }

        public List<Company> GetSubscriptionsForUser()
        {
            return new CompanyManager().GetCompanies().Where(c => LoggedInUser.Subscriptions.Contains(c.Id)).ToList();
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        internal static bool IsUserSubscribed(string userid, string companyid)
        {
            return FindUser(userid).Subscriptions.Contains(companyid);
        }

        internal static void SetUserSubscription(bool value, string userid, string companyid)
        {
            if (!value)
            {
                FindUser(userid).Subscriptions.Remove(companyid);
            }
            if (value)
            {
                FindUser(userid).Subscriptions.Add(companyid);
            }
        }

        internal static bool IsUserLoggedIn()
        {
            return LoggedInUser != null;
        }

        public void LogOut()
        {
            LoggedInUser = null;
        }

        internal static void UpdateUser(User user)
        {
            _users.Replace(u => u.Id.Equals(user.Id), user);
        }
    }
}
