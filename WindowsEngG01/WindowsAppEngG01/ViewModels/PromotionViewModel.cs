using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;

namespace WindowsAppEngG01.ViewModels
{
    public class PromotionViewModel : INotifyPropertyChanged
    {

        private Company Company { get; set; }
        private int Identifier { get; set; }

        internal void SetCompany(Company company)
        {
            Company = company;
        }

        internal void SetPromotionIdentifier(int identifier)
        {
            Identifier = identifier;
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
