using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.Commands;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.Views
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public DelegateCommand DeleteNotificationCommand { get; set; }
        private string _userId;

        public List<Notification> Notifications
        {
            get
            {
                return NotificationManager.GetNotificationsForUser(UserId);
            }
        }

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; NotifyPropertyChanged(nameof(UserId)); }
        }

        public NotificationsViewModel()
        {
            DeleteNotificationCommand = new DelegateCommand(DeleteNotification);
        }

        private void DeleteNotification(object parameter)
        {
            var temp = (Notification)parameter;

            NotificationManager.DeleteNotificationAsync(temp.Id);
            NotifyPropertyChanged(nameof(Notifications));
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
