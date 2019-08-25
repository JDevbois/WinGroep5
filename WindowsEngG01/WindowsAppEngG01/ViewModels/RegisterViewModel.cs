using SharedLib;
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
    public class RegisterViewModel : INotifyPropertyChanged
    {
        #region properties
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _street;
        private string _number;
        private string _postalcode;
        private string _city;
        private string _password;
        private string _passwordConfirm;

        public DelegateCommand RegisterCommand { get; set; }

        private String feedback;

        public string Email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(nameof(Email)); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; NotifyPropertyChanged(nameof(FirstName)); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; NotifyPropertyChanged(nameof(LastName)); }
        }

        public string Street
        {
            get { return _street; }
            set { _street = value; NotifyPropertyChanged(nameof(Street)); }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; NotifyPropertyChanged(nameof(Number)); }
        }

        public string PostalCode
        {
            get { return _postalcode; }
            set { _postalcode = value; NotifyPropertyChanged(nameof(PostalCode)); }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; NotifyPropertyChanged(nameof(City)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyPropertyChanged(nameof(Password)); }
        }

        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { _passwordConfirm = value; NotifyPropertyChanged(nameof(PasswordConfirm)); }
        }

        public string Feedback { get => feedback; set
            {
                feedback = value;
                NotifyPropertyChanged(nameof(Feedback));
            }
        }
        #endregion

        public RegisterViewModel()
        {
            this.RegisterCommand = new DelegateCommand(Register, CanRegister);
        }

        public void Register(object parameter)
        {
            try
            {
                if (Password != PasswordConfirm)
                {
                    throw new Exception("Password fields do not match.");
                }
                if (!NoPropertyIsNull())
                {
                    throw new Exception("Please make sure you filled out all fields.");
                }
                User user = new User
                {
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Street = Street,
                    Number = Number,
                    PostalCode = PostalCode,
                    City = City,
                    Password = Password
                };

                new UserManager().AddUser(user);
                UserManager.LoggedInUser = new UserManager().FindUser(Email, Password);
                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(AccountPage));
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Feedback = e.Message;
            }
        }

        private bool NoPropertyIsNull()
        {
            return !(String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(Street) || String.IsNullOrEmpty(Number)
                || String.IsNullOrEmpty(PostalCode) || String.IsNullOrEmpty(City) || String.IsNullOrEmpty(Password));
        }

        public bool CanRegister(object parameter)
        {
            return !UserManager.IsUserLoggedIn();
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
