using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private List<Company> _subscriptions;

        public List<Company> Subscriptions
        {
            get {
                return _subscriptions;
            }
            set {
                _subscriptions = value; NotifyPropertyChanged(nameof(Subscriptions));
            }
        }

        private List<Company> _spotlightCompanies;

        public List<Company> SpotlightCompanies {
            get {
                return _spotlightCompanies;
            }
            set {
                _spotlightCompanies = value; NotifyPropertyChanged(nameof(SpotlightCompanies));
            } }

        public User LoggedInUser { get; set; }

        private int _selectedSpotlightCompanyId;

        public int SelectedSpotlightCompanyId {
            get { return _selectedSpotlightCompanyId; }
            set { _selectedSpotlightCompanyId = value; NotifyPropertyChanged(nameof(SelectedSpotlightCompanyId)); }
        }

        private int _selectedSubscriptionCompanyId;

        public int SelectedSubscriptionCompanyId
        {
            get { return _selectedSubscriptionCompanyId; }
            set { _selectedSubscriptionCompanyId = value; NotifyPropertyChanged(nameof(SelectedSubscriptionCompanyId)); }
        }

        public HomeViewModel()
        {
            this.LoggedInUser = UserManager.LoggedInUser;
            this.SpotlightCompanies = new CompanyManager().GetSpotlightCompanies();
            if (UserManager.IsUserLoggedIn())
            {
                this.Subscriptions = new UserManager().GetSubscriptionsForUser();
            }
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
