using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using WindowsAppEngG01.DataManagers;
using WindowsAppEngG01.ViewModels;
using WindowsAppEngG01.Views;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsAppEngG01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
        }

        #region nvTopNavigationView eventhandlers
        private void NvTopLevelNav_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home_Page")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(HomePage));
        }

        private void NvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void NvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                TextBlock ItemContent = args.InvokedItem as TextBlock;
                if (ItemContent != null)
                {
                    switch (ItemContent.Tag)
                    {
                        case "Nav_Home":
                            contentFrame.Navigate(typeof(HomePage));
                            break;
                        case "Nav_Notifications":
                            if (UserManager.IsUserLoggedIn())
                                contentFrame.Navigate(typeof(NotificationsPage));
                            else
                                contentFrame.Navigate(typeof(NotLoggedInPage));
                            break;
                        case "Nav_Search":
                            contentFrame.Navigate(typeof(SearchPage));
                            break;
                        case "Nav_Dashboard":
                            if (UserManager.IsUserLoggedIn())
                                contentFrame.Navigate(typeof(DashboardPage));
                            else
                                contentFrame.Navigate(typeof(NotLoggedInPage));
                            break;
                        case "Nav_Account":
                            //TODO doesn't work properly
                            if (ViewModel.IsAuthenticated is false)
                                contentFrame.Navigate(typeof(LoginPage));
                            else
                                contentFrame.Navigate(typeof(AccountPage));
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
