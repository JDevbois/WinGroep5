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
using WindowsAppEngG01.Utils;
using WindowsAppEngG01.Views;

namespace WindowsAppEngG01.ViewModels
{
    public class EditPromotionViewModel: INotifyPropertyChanged
    {
        private Promotion _promotion;
        private int _identifier;
        private String _companyId;
        private String _pDFUri;

        public Promotion Promotion
        {
            get { return _promotion; }
            set { _promotion = value; NotifyPropertyChanged(nameof(Promotion)); }
        }

        public int Identifier
        {
            get { return _identifier; }
            set { _identifier = value; NotifyPropertyChanged(nameof(Identifier)); }
        }

        public String CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; NotifyPropertyChanged(nameof(CompanyId)); }
        }

        public String PromotionTypeTitle
        {
            get
            {
                if (Identifier == (int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT)
                {
                    return "Add event";
                }
                else if (Identifier == (int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE)
                {
                    return "Add Discount Coupon";
                }
                else return "Add Promotion";
            }
        }
        public String Name
        {
            get { return Promotion.Name; }
            set { Promotion.Name = value; NotifyPropertyChanged(nameof(Name)); }
        }
        public String Description
        {
            get { return Promotion.Description; }
            set { Promotion.Description = value; NotifyPropertyChanged(nameof(Description)); }
        }
        public DateTimeOffset StartDate
        {
            get { return Promotion.StartDate; }
            set { Promotion.StartDate = value; NotifyPropertyChanged(nameof(StartDate)); }
        }
        public DateTimeOffset EndDate
        {
            get { return Promotion.EndDate; }
            set { Promotion.EndDate = value; NotifyPropertyChanged(nameof(EndDate)); }
        }
        public TimeSpan StartTime
        {
            get { return Promotion.StartTime; }
            set { Promotion.StartTime = value; NotifyPropertyChanged(nameof(StartTime)); }
        }
        public TimeSpan EndTime
        {
            get { return Promotion.EndTime; }
            set { Promotion.EndTime = value; NotifyPropertyChanged(nameof(EndTime)); }
        }
        public String PDFUri
        {
            get { return _pDFUri; }
            set { _pDFUri = value; NotifyPropertyChanged(nameof(PDFUri)); }
        }
        public DelegateCommand SubmitPromotionCommand { get; set; }
        public DelegateCommand DeletePromotionCommand { get; set; }

        public EditPromotionViewModel()
        {
            SubmitPromotionCommand = new DelegateCommand(SubmitPromotion);
            DeletePromotionCommand = new DelegateCommand(DeletePromotion);
        }

        private void SubmitPromotion(object parameter)
        {
            CompanyManager.UpdatePromotion(CompanyId, Promotion, Identifier);
            Debug.WriteLine("Update Promotion Called");

            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(DashboardPage));
        }

        private void DeletePromotion(object parameter)
        {
            CompanyManager.DeletePromotion(CompanyId, Promotion, Identifier);
            Debug.WriteLine("Delete Promotion Called");

            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(DashboardPage));
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
