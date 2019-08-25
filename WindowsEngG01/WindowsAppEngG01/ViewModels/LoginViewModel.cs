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
        private String _feedback;

        public string Email {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }

        public bool IsAuthenticated { get; set; }
        public DelegateCommand LoginCommand { get; set; }

        public String Feedback
        {
            get { return _feedback; }
            set { _feedback = value; NotifyPropertyChanged(nameof(Feedback)); }
        }

        public LoginViewModel()
        {
            IsAuthenticated = UserManager.IsUserLoggedIn();
            LoginCommand = new DelegateCommand(Login, CanLogin);
        }

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;

            try
            {
                if (!NoFieldsAreNull(parameter))
                {
                    throw new Exception("Please make sure to fill out all fields.");
                }
                UserManager.LoggedInUser = new UserManager().FindUser(Email, clearTextPassword);
                IsAuthenticated = UserManager.IsUserLoggedIn();

                NotifyPropertyChanged("IsAuthenticated");
                LoginCommand.RaiseCanExecuteChanged();

                Email = string.Empty;
                passwordBox.Password = string.Empty;

                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(AccountPage));
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
                Feedback = "No user found with these credentials.";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetType().ToString());
                Debug.WriteLine(e.Message);
                Feedback = e.Message;

                Email = string.Empty;
                passwordBox.Password = string.Empty;
            }
        }

        private bool NoFieldsAreNull(object parameter)
        {
            return !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(((PasswordBox)parameter).Password);
        }

        public bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
