using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsAppEngG01.Commands;
using WindowsAppEngG01.DataManagers;
using WindowsAppEngG01.Utils;
using WindowsAppEngG01.Views;

namespace WindowsAppEngG01.ViewModels
{
    public class AddPromotionViewModel : INotifyPropertyChanged
    {
        private Company Company { get; set; }

        private int Identifier
        {
            get { return _identifier; }
            set {
                _identifier = value;
                NotifyPropertyChanged(nameof(Identifier));
                SetPromotionType(value);
            }
        }

        public String Feedback
        {
            get { return _feedback; }
            set { _feedback = value; NotifyPropertyChanged(nameof(Feedback)); }
        }

        private void SetPromotionType(int value)
        {
            if (value == (int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT)
            {
                Promotion = new Event();
            }
            else if (value == (int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE)
            {
                Promotion = new DiscountCoupon();
            }
            else
            {
                Promotion = new Promotion();
            }
        }

        private Promotion Promotion { get; set; }

        private int _identifier;
        private string _feedback;

        public DelegateCommand SubmitPromotionCommand { get; set; }
        public DelegateCommand UploadPDFCommand { get; set; }

        public string Name { get => Promotion.Name; set
            {
                Promotion.Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string Description { get => Promotion.Description; set
            {
                Promotion.Description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        public DateTimeOffset StartDate { get => Promotion.StartDate; set
            {
                Promotion.StartDate = value;
                NotifyPropertyChanged(nameof(StartDate));
            }
        }

        public DateTimeOffset EndDate { get => Promotion.EndDate; set
            {
                Promotion.EndDate = value;
                NotifyPropertyChanged(nameof(EndDate));
            }
        }

        public Uri PDFUri { get => Promotion.PDFUri; set
            {
                Promotion.PDFUri = value;
                NotifyPropertyChanged(nameof(PDFUri));
            }
        }

        public TimeSpan StartTime { get => Promotion.StartTime; set
            {
                Promotion.StartTime = value;
                NotifyPropertyChanged(nameof(StartTime));
            }
        }

        public TimeSpan EndTime { get => Promotion.EndTime; set
            {
                Promotion.EndTime = value;
                NotifyPropertyChanged(nameof(EndTime));
            }
        }

        internal void SetCompany(Company company)
        {
            Company = company;
        }

        internal void SetPromotionIdentifier(int identifier)
        {
            Identifier = identifier;
        }

        public string PromotionTypeTitle
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

        public bool NoFieldsAreNull()
        {
            return !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Description) && !String.IsNullOrEmpty(PDFUri.LocalPath);
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
                    if (Identifier == (int)AddPromotionPassThroughElement.IDENTIFIERS.EVENT)
                    {
                        CompanyManager.AddEvent(Company.Id, (Event)Promotion);
                    }
                    else if (Identifier == (int)AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE)
                    {
                        CompanyManager.AddDiscountCoupon(Company.Id, (DiscountCoupon)Promotion);
                    }
                    else
                    {
                        CompanyManager.AddPromotion(Company.Id, Promotion);
                    }
                    ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(DashboardPage));
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Feedback = e.Message;
            }
        }

        public AddPromotionViewModel()
        {
            SetPromotionType(Identifier);

            SubmitPromotionCommand = new DelegateCommand(SubmitPromotion);
            UploadPDFCommand = new DelegateCommand(UploadPDFAsync);
        }

        private async void UploadPDFAsync(object parameter)
        {
            try
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
                    Debug.WriteLine("Picked pdf: " + file.Path);
                }
                else
                {
                    Debug.WriteLine("Operation cancelled.");
                    throw new Exception("No pdf was selected");
                }
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Feedback = e.Message;
            }
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
