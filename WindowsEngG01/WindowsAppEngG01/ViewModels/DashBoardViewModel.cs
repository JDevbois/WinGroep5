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
            set { _selectedCompany = value; NotifyPropertyChanged(nameof(SelectedCompany)); }
        }

        public List<String> AllowedTypes { get; set; }

        public DashBoardViewModel()
        {
            Companies = new CompanyManager().GetCompanies().Where(c => c.UserId == UserManager.LoggedInUser.Id).ToList();
            AddCompanyCommand = new DelegateCommand(AddCompany);
            SaveChangesCommand = new DelegateCommand(SaveChanges);
        }

        private void AddCompany(object parameter)
        {
            new CompanyManager().AddCompany(new Company
            {
                //TODO should be 'empty'
                UserId = UserManager.LoggedInUser.Id,
                Name = "Placeholder",
                Logo = new Uri("https://img.freepik.com/free-vector/santa-clause-in-costume-carrying-sack_1262-15966.jpg?size=338&ext=jpg")
            });
            Companies = new CompanyManager().GetCompanies().Where(c => c.UserId == UserManager.LoggedInUser.Id).ToList();
            Debug.WriteLine(Companies);
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
