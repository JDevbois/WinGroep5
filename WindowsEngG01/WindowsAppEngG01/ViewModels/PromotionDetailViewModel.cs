using SharedLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAppEngG01.Commands;

namespace WindowsAppEngG01.ViewModels
{
    public class PromotionDetailViewModel : INotifyPropertyChanged
    {

        public String Name { get; set; }
        public String Description { get; set; }
        public String Start { get; set; }
        public String End { get; set; }
        public Uri PDF { get; set; }
        public String PromotionId { get; set; }
        public String CompanyId { get; set; }
        private Promotion promotion { get; set; }

        public DelegateCommand DownloadPDFCommand { get; set; }
        public DelegateCommand BackToCompanyCommand { get; set; }

        private void DownloadPDF(object parameter)
        {

        }

        private void BackToCompany(object parameter)
        {

        }

        public PromotionDetailViewModel()
        {
            DownloadPDFCommand = new DelegateCommand(DownloadPDF);
            BackToCompanyCommand = new DelegateCommand(BackToCompany);

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
