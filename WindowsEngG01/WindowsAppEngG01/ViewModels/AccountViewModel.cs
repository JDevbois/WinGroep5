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
    public class AccountViewModel : INotifyPropertyChanged
    {
        private User user = UserManager.LoggedInUser;
        private string oldPassword;
        private string newPassword;
        private string confirmPassword;

        public DelegateCommand LogoutCommand { get; set; }

        public string Email { get => user.Email; set
            {
                user.Email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

        public string FirstName { get => user.FirstName; set
            {
                user.FirstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName { get => user.LastName; set
            {
                user.LastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        public string Street { get => user.Street; set
            {
                user.Street = value;
                NotifyPropertyChanged(nameof(Street));
            }
        }

        public string Number { get => user.Number; set
            {
                user.Number = value;
                NotifyPropertyChanged(nameof(Number));
            }
        }

        public string PostalCode { get => user.PostalCode; set
            {
                user.PostalCode = value;
                NotifyPropertyChanged(nameof(PostalCode));
            }
        }

        public string City { get => user.City; set
            {
                user.City = value;
                NotifyPropertyChanged(nameof(City));
            }
        }

        public string OldPassword { get => oldPassword; set
            {
                oldPassword = value;
                NotifyPropertyChanged(nameof(OldPassword));
            }
        }

        public string NewPassword { get => newPassword; set
            {
                newPassword = value;
                NotifyPropertyChanged(nameof(NewPassword));
            }
        }

        public string ConfirmPassword { get => confirmPassword; set
            {
                confirmPassword = value;
                NotifyPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public bool IsAuthenticated { get; set; }

        public void Logout(object parameter)
        {
            try
            {
                new UserManager().LogOut();
                IsAuthenticated = UserManager.IsUserLoggedIn();

                NotifyPropertyChanged("IsAuthenticated");
                LogoutCommand.RaiseCanExecuteChanged();
                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(LoginPage));
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

        public AccountViewModel()
        {
            IsAuthenticated = UserManager.IsUserLoggedIn();
            LogoutCommand = new DelegateCommand(Logout, CanLogout);
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
