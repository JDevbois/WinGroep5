using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using WindowsAppEngG01.Commands;
using WindowsAppEngG01.Views;

namespace WindowsAppEngG01.ViewModels
{
    public class PromotionDetailViewModel : INotifyPropertyChanged
    {
        private String _name;
        private String _description;
        private String _start;
        private String _end;
        private Uri _pDF;
        private String _promotionId;
        private String _companyId;

        public String Name
        {
            get { return Promotion.Name; }
        }
        public String Description
        {
            get { return Promotion.Description; }
        }
        public String Start
        {
            get
            {
                return String.Format("{0} {1}", Promotion.StartDate.ToString("D"), Promotion.StartTime.ToString(@"hh\:mm"));
            }
        }
        public String End
        {
            get
            {
                return String.Format("{0} {1}", Promotion.EndDate.ToString("D"), Promotion.EndTime.ToString(@"hh\:mm"));
            }
        }
        public Uri PDF
        {
            get { return Promotion.PDFUri; }
        }
        public String PromotionId
        {
            get { return Promotion.Id; }
            set { _promotionId = value; }
        }
        public String CompanyId
        {
            get { return Promotion.CompanyId; }
            set { _companyId = value; }
        }

        internal void SetPromotion(Promotion promotion)
        {
            Promotion = promotion;
        }

        private Promotion Promotion { get; set; }

        public DelegateCommand DownloadPDFCommand { get; set; }
        public DelegateCommand BackToCompanyCommand { get; set; }

        #region PDF
        private void DownloadPDF(object parameter)
        {
            OpenLocal();
        }

        public async void OpenLocal()
        {
            StorageFile f = await
                StorageFile.GetFileFromApplicationUriAsync(PDF);
            PdfDocument doc = await PdfDocument.LoadFromFileAsync(f);

            Load(doc);
        }

        public async void OpenRemote()
        {
            HttpClient client = new HttpClient();
            var stream = await
                client.GetStreamAsync("http://www.adobe.com/content/dam/Adobe/en/accessibility/products/acrobat/pdfs/acrobat-x-accessible-pdf-from-word.pdf");
            var memStream = new MemoryStream();
            await stream.CopyToAsync(memStream);
            memStream.Position = 0;
            PdfDocument doc = await PdfDocument.LoadFromStreamAsync(memStream.AsRandomAccessStream());

            Load(doc);
        }

        async void Load(PdfDocument pdfDoc)
        {
            PdfPages.Clear();

            for (uint i = 0; i < pdfDoc.PageCount; i++)
            {
                BitmapImage image = new BitmapImage();

                var page = pdfDoc.GetPage(i);

                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await page.RenderToStreamAsync(stream);
                    await image.SetSourceAsync(stream);
                }

                PdfPages.Add(image);
            }
        }

        public ObservableCollection<BitmapImage> PdfPages
        {
            get;
            set;
        } = new ObservableCollection<BitmapImage>();
        #endregion

        private void BackToCompany(object parameter)
        {
            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(CompanyDetailsPage), CompanyId);
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
