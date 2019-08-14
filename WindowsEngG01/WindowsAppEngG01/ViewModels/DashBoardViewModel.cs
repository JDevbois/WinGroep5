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
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        private List<Company> _companies;
        private Company _selectedCompany;
        private bool _companySelected;

        public DelegateCommand AddCompanyCommand { get; set; }
        public DelegateCommand DeleteCompanyCommand { get; set; }
        public DelegateCommand SaveChangesCommand { get; set; }
        public DelegateCommand EditPromotionCommand { get; set; }
        public DelegateCommand EditEventCommand { get; set; }
        public DelegateCommand EditDiscountCouponCommand { get; set; }

        public List<Company> Companies
        {
            get { return _companies; }
            set { _companies = value; NotifyPropertyChanged(nameof(Companies)); }
        }

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set { _selectedCompany = value; CompanySelected = true; NotifyPropertyChanged(nameof(CompanySelected)); NotifyPropertyChanged(nameof(SelectedCompany)); }
        }

        public List<String> AllowedTypes { get; set; }
        public bool CompanySelected { get { return _companySelected;} set { _companySelected = value; NotifyPropertyChanged(nameof(CompanySelected)); } }

        public DashBoardViewModel()
        {
            Companies = new CompanyManager().GetCompanies().Where(c => c.UserId.Equals(UserManager.LoggedInUser.Id)).ToList();
            AddCompanyCommand = new DelegateCommand(AddCompany);
            DeleteCompanyCommand = new DelegateCommand(DeleteCompany);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            EditPromotionCommand = new DelegateCommand(EditPromotion);
            EditEventCommand = new DelegateCommand(EditEvent);
            EditDiscountCouponCommand = new DelegateCommand(EditDiscountCoupon);
            SelectedCompany = CreateTempCompany();
            CompanySelected = false;
        }

        private void DeleteCompany(object parameter)
        {
            CompanyManager.DeleteCompany(SelectedCompany.Id);
            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(DashboardPage));
        }

        private void AddCompany(object parameter)
        {
            var temp = CreateTempCompany();
            new CompanyManager().AddCompany(temp);
            Companies = new CompanyManager().GetCompanies().Where(c => c.UserId.Equals(UserManager.LoggedInUser.Id)).ToList();
            Debug.WriteLine(Companies);
        }

        private Company CreateTempCompany()
        {
            return new Company
            {
                UserId = UserManager.LoggedInUser.Id,
                Name = "Placeholder"
            };
        }

        private void SaveChanges(object parameter)
        {
            new CompanyManager().UpdateCompany((Company)parameter);
        }

        private void EditPromotion(object parameter)
        {
            var paramPromotion = (Promotion)parameter;

            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(EditPromotionPage), new Utils.EditPromotionPassThroughElement(paramPromotion, SelectedCompany, (int)Utils.AddPromotionPassThroughElement.IDENTIFIERS.PROMOTION));
            Debug.WriteLine("Edit Promotion Called");
        }

        private void EditEvent(object parameter)
        {
            var paramPromotion = (Event)parameter;

            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(EditPromotionPage), new Utils.EditPromotionPassThroughElement(paramPromotion, SelectedCompany, (int)Utils.AddPromotionPassThroughElement.IDENTIFIERS.EVENT));
            Debug.WriteLine("Edit Event Called");
        }

        private void EditDiscountCoupon(object parameter)
        {
            var paramPromotion = (DiscountCoupon)parameter;

            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(EditPromotionPage), new Utils.EditPromotionPassThroughElement(paramPromotion, SelectedCompany, (int)Utils.AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE));
            Debug.WriteLine("Edit Discount Coupon Called");
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
