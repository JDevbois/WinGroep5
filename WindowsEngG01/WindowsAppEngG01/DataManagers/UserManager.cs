using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.DataManagers
{
    //TODO not storing passwords in clear text
    public class UserManager
    {
        public static User LoggedInUser { get; set; }
        public void AddUser(string email, string password)
        {

        }
        
        public User FindUser(string email, string password)
        {
            //TODO get data from database
            return new User(email);
        }

        internal static bool IsUserLoggedIn()
        {
            return LoggedInUser != null ? true : false;
        }

        internal static void LogOut()
        {
            LoggedInUser = null;
        }
    }
}
