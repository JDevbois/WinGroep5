using SharedLib;
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
using WindowsAppEngG01.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsAppEngG01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class HomePage : Page
    {
        public HomeViewModel ViewModel = new HomeViewModel();

        public HomePage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void CrsItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (crsSpotlight.SelectedIndex != -1)
            {
                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(CompanyDetailsPage), ViewModel.SelectedSpotlightCompanyId);
            }
        }

        private void CrsItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(crsSpotlight.SelectedIndex != -1)
            {
                ViewModel.SelectedSpotlightCompanyId = ViewModel.SpotlightCompanies[crsSpotlight.SelectedIndex].Id;
            }
            Debug.WriteLine(crsSpotlight.SelectedIndex);
        }

        #region LVSubscriptions event handlers
        private void LvSubscriptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvSubscriptions.SelectedIndex != -1)
            {
                ViewModel.SelectedSubscriptionCompanyId = ViewModel.Subscriptions[lvSubscriptions.SelectedIndex].Id;
            }
            Debug.WriteLine(lvSubscriptions.SelectedIndex);
        }

        private void LvSubscriptionsItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (lvSubscriptions.SelectedIndex != -1)
            {
                ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(CompanyDetailsPage), ViewModel.SelectedSubscriptionCompanyId);
            }
        }
        #endregion
    }
}
