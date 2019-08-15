using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.ViewModels
{
    public class CompanyDetails : INotifyPropertyChanged
    {
        private Company Company { get; set; }
        private string _name;
        private string _city;
        private string _street;
        private string _postalCode;
        private string _number;
        private List<Promotion> _promotions;
        private List<Event> _events;
        private string _openingHours;
        private string _type;
        private string _website;
        private Uri _logo;

        public string Name
        {
            get { return Company.Name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public string City { get => Company.City; set => _city = value; }
        public string Street { get => Company.Street; set => _street = value; }
        public string PostalCode { get => Company.PostalCode; set => _postalCode = value; }
        public string Number { get => Company.Number; set => _number = value; }

        public void SetCompany(Company company)
        {
            Company = company;
            NotifyPropertyChanged(nameof(Company));
        }

        public List<Promotion> Promotions { get => Company.Promotions; }
        public List<Event> Events { get => Company.Events; }
        public List<DiscountCoupon> DiscountCoupons { get => Company.DiscountCoupons; }
        public string OpeningHours { get => Company.OpeningHours; set => _openingHours = value; }
        public string Type { get => Company.Type; set => _type = value; }
        public string Website { get => Company.Website; set => _website = value; }
        public Uri Logo { get => Company.Logo; set => _logo = value; }
        public bool IsLoggedIn { get => UserManager.IsUserLoggedIn(); }

        public bool IsUserSubscribed
        {
            get
            {
                if (IsLoggedIn)
                    return UserManager.IsUserSubscribed(UserManager.LoggedInUser.Id, Company.Id);
                else
                    return false;
            }

            set
            {
                UserManager.SetUserSubscription(value, UserManager.LoggedInUser.Id, Company.Id);
                NotifyPropertyChanged(nameof(IsUserSubscribed));
            }
        }
        public CompanyDetails()
        {

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
