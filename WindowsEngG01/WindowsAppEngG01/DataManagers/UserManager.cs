using Newtonsoft.Json;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.Utils;

namespace WindowsAppEngG01.DataManagers
{
    //TODO not storing passwords in clear text
    public class UserManager
    {
        private static List<User> _users = new List<User>
            {
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
        public static string POSTString = "http://localhost:54089/api/Users";

        public async Task AddUserAsync(User user)
        {
            if (UseBackend.Backend)
            {
                var userJson = JsonConvert.SerializeObject(user);

                HttpClient client = new HttpClient();
                var res = await client.PostAsync(POSTString, new StringContent(userJson, System.Text.Encoding.UTF8, "application/json")).ConfigureAwait(false);

            } else
            {
                _users.Add(user);
            }
        }

        public User FindUser(
            string email,
            string password)
        {
            return GetUsers().First(u => u.Email == email && u.Password == password);
        }

        public static User FindUser(string id)
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var json = client.GetStringAsync(new Uri(String.Format("{0}/{1}", POSTString, id))).Result;
                var res = JsonConvert.DeserializeObject<User>(json);
                return res;
            } else
            {
                return new UserManager().GetUsers().First(u => u.Id.Equals(id));
            }
        }

        public List<Company> GetSubscriptionsForUser()
        {
            return new CompanyManager().GetCompanies().Where(c => LoggedInUser.Subscriptions.Contains(c.Id)).ToList();
        }

        public List<User> GetUsers()
        {
            if (UseBackend.Backend)
            {
                HttpClient client = new HttpClient();
                var json = client.GetStringAsync(new Uri(POSTString)).Result;
                var lst = JsonConvert.DeserializeObject<List<User>>(json);
                return lst;
            }
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

        internal static async Task UpdateUserAsync(User user)
        {
            if (UseBackend.Backend)
            {
                var userJson = JsonConvert.SerializeObject(user);

                HttpClient client = new HttpClient();
                var res = await client.PutAsync(String.Format("{0}/{1}", POSTString, user.Id), new StringContent(userJson, System.Text.Encoding.UTF8, "application/json"));
            }
            _users.Replace(u => u.Id.Equals(user.Id), user);
        }
    }
}
