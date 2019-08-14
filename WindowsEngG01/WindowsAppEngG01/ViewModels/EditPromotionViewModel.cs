using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAppEngG01.ViewModels
{
    public class EditPromotionViewModel: INotifyPropertyChanged
    {
        public Promotion Promotion { get; set; }

        public EditPromotionViewModel()
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
