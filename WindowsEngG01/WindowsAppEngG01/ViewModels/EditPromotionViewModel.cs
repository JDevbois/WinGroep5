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
        private String _feedback;

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

        public String Feedback
        {
            get { return _feedback; }
            set { _feedback = value; NotifyPropertyChanged(nameof(Feedback)); }
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
        public Uri PDFUri
        {
            get { return Promotion.PDFUri; }
            set { Promotion.PDFUri = value; NotifyPropertyChanged(nameof(PDFUri)); }
        }
        public DelegateCommand SubmitPromotionCommand { get; set; }
        public DelegateCommand DeletePromotionCommand { get; set; }
        public DelegateCommand UploadPDFCommand { get; set; }

        public EditPromotionViewModel()
        {
            SubmitPromotionCommand = new DelegateCommand(SubmitPromotion);
            DeletePromotionCommand = new DelegateCommand(DeletePromotion);
            UploadPDFCommand = new DelegateCommand(UploadPDFAsync);
        }

        public bool NoFieldsAreNull()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description);
        }

        private bool IsStartNotLaterThanEnd()
        {
            var startTemp = StartDate;
            var endTemp = EndDate;

            startTemp.AddHours(StartTime.Hours);
            startTemp.AddMinutes(StartTime.Minutes);

            endTemp.AddHours(EndTime.Hours);
            endTemp.AddMinutes(EndTime.Minutes);

            return DateTimeOffset.Compare(startTemp, endTemp) < 0;
        }

        private void SubmitPromotion(object parameter)
        {
            try
            {
                if (NoFieldsAreNull())
                {
                    throw new Exception("Please make sure none of the fields are empty");
                }
                if (IsStartNotLaterThanEnd())
                {
                    throw new Exception("The Start Time and Date cannot be later than the End Time and Date");
                }
                CompanyManager.UpdatePromotion(CompanyId, Promotion, Identifier);
                Debug.WriteLine("Update Promotion Called");

                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(DashboardPage));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Feedback = e.Message;
            }
        }

        private void DeletePromotion(object parameter)
        {
            CompanyManager.DeletePromotion(CompanyId, Promotion, Identifier);
            Debug.WriteLine("Delete Promotion Called");

            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(DashboardPage));
        }

        private async void UploadPDFAsync(object parameter)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".pdf");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                PDFUri = new Uri(file.Path);
                Debug.WriteLine("Picked photo: " + file.Path);
            }
            else
            {
                Debug.WriteLine("Operation cancelled.");
            }
            Debug.WriteLine("Upload PDF called");
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
