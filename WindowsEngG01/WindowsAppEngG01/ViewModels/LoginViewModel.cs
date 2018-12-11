using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsAppEngG01.Commands;
using WindowsAppEngG01.DataManagers;
using WindowsAppEngG01.Views;

namespace WindowsAppEngG01.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _email;
        public string Email {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged("Email"); }
        }
        public bool IsAuthenticated { get; set; }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        public LoginViewModel()
        {
            IsAuthenticated = UserManager.IsUserLoggedIn();
            LoginCommand = new DelegateCommand(Login, CanLogin);
            LogoutCommand = new DelegateCommand(Logout, CanLogout);
        }

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            try
            {
                UserManager.LoggedInUser = new UserManager().FindUser(Email, clearTextPassword);
                IsAuthenticated = UserManager.IsUserLoggedIn();

                NotifyPropertyChanged("IsAuthenticated");
                LoginCommand.RaiseCanExecuteChanged();
                LogoutCommand.RaiseCanExecuteChanged();

                Email = string.Empty;
                passwordBox.Password = string.Empty;

                ((Window.Current.Content as Frame).Content as MainPage).contentFrame.Navigate(typeof(AccountPage));

            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        public void Logout(object parameter)
        {

            try
            {
                UserManager.LogOut();
                IsAuthenticated = UserManager.IsUserLoggedIn();

                NotifyPropertyChanged("IsAuthenticated");
                LoginCommand.RaiseCanExecuteChanged();
                LogoutCommand.RaiseCanExecuteChanged();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
