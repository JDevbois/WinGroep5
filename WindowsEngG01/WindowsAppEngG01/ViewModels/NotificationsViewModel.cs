using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.Views
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {

        public List<Notification> Notifications
        {
            get
            {
                return NotificationManager.GetNotificationsForUser(UserId);
            }
        }

        public string UserId { get; set; }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
