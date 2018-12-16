using System;
using System.Collections.Generic;
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
using WindowsAppEngG01.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsAppEngG01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashBoardViewModel ViewModel = new DashBoardViewModel();
        public DashboardPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            switch (e.Section.Name)
            {
                case "Sports":
                    Frame.Navigate(typeof(AccountPage));
                    break;
                case "Tech":
                    Frame.Navigate(typeof(AccountPage));
                    break;
            }
        }

        private void StpAddCompany_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void LvCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
