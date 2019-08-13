﻿using System;
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
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }

        public bool IsAuthenticated { get; set; }
        public DelegateCommand LoginCommand { get; set; }

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
                UserManager.LoggedInUser = new UserManager().FindUser(Email, clearTextPassword);
                IsAuthenticated = UserManager.IsUserLoggedIn();

                NotifyPropertyChanged("IsAuthenticated");
                LoginCommand.RaiseCanExecuteChanged();

                Email = string.Empty;
                passwordBox.Password = string.Empty;

                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(AccountPage));
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
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
