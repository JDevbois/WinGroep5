using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsAppEngG01.DataManagers;
using WindowsAppEngG01.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsAppEngG01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompanyDetailsPage : Page
    {

        private CompanyDetails CompanyDetails = new CompanyDetails();

        public CompanyDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id = (int)e.Parameter;
            CompanyDetails.Company = new CompanyManager().FindCompanyById(id);
            Debug.WriteLine("company name: " + CompanyDetails.Company.Name);
            Debug.WriteLine("company id: " + CompanyDetails.Company.Id);
            this.DataContext = CompanyDetails;
        }


    }
}
