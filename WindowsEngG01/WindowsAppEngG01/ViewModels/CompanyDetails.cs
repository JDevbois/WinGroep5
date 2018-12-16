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
        public Company Company { get; set; }
        private string _name;

        public string Name
        {
            get { return Company.Name; }
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
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
