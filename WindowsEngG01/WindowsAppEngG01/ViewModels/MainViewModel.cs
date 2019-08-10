using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.ViewModels
{
    public class MainViewModel
    {
        private bool isAuthenticated;
        public MainViewModel()
        {

        }

        public bool IsAuthenticated { get => UserManager.IsUserLoggedIn(); set => isAuthenticated = value; }
    }
}