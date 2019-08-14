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

namespace WindowsAppEngG01.ViewModels
{
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        private List<Company> _companies;
        private Company _selectedCompany;
        private bool _companySelected;

        public DelegateCommand AddCompanyCommand { get; set; }

        public DelegateCommand SaveChangesCommand { get; set; }

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
            SaveChangesCommand = new DelegateCommand(SaveChanges);
            SelectedCompany = CreateTempCompany();
            CompanySelected = false;
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

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
