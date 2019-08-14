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
using WindowsAppEngG01.DataManagers;
using WindowsAppEngG01.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsAppEngG01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        public DashBoardViewModel ViewModel
        {
            get { return (DashBoardViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public DashboardPage()
        {
            ViewModel = new DashBoardViewModel();
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(DashBoardViewModel), typeof(DashboardPage), new PropertyMetadata(0));

        private void LvCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SetSelectedCompany((Company)((GridView)FindChildByName(this, "LvCompanies")).SelectedItem);
        }

        static DependencyObject FindChildByName(DependencyObject from, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(from);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(from, i);
                if (child is FrameworkElement && ((FrameworkElement)child).Name == name)
                    return child;

                var result = FindChildByName(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }

        private void BtnEvent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(AddPromotionPage), new Utils.AddPromotionPassThroughElement(ViewModel.SelectedCompany.Id, (int)Utils.AddPromotionPassThroughElement.IDENTIFIERS.EVENT));
        }

        private void BtnDiscount_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(AddPromotionPage), new Utils.AddPromotionPassThroughElement(ViewModel.SelectedCompany.Id, (int)Utils.AddPromotionPassThroughElement.IDENTIFIERS.DISCOUNTCODE));
        }

        private void BtnPromotion_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(AddPromotionPage), new Utils.AddPromotionPassThroughElement(ViewModel.SelectedCompany.Id, (int)Utils.AddPromotionPassThroughElement.IDENTIFIERS.PROMOTION));
        }
    }
}
