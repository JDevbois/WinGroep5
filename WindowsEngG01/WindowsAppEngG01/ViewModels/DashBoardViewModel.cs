using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.DataManagers;

namespace WindowsAppEngG01.ViewModels
{
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        private List<Company> _companies = new CompanyManager().GetCompanies();

        public List<Company> Companies
        {
            get { return _companies; }
            set { _companies = value; NotifyPropertyChanged(nameof(Companies)); }
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
