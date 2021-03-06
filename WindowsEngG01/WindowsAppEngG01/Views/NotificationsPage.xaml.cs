﻿using SharedLib;
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
using WindowsAppEngG01.DataManagers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsAppEngG01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationsPage : Page
    {

        public NotificationsViewModel ViewModel
        {
            get { return (NotificationsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
    DependencyProperty.Register("ViewModel", typeof(NotificationsViewModel), typeof(NotificationsPage), new PropertyMetadata(0));


        public NotificationsPage()
        {
            ViewModel = new NotificationsViewModel();
            ViewModel.UserId = UserManager.LoggedInUser.Id;
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var notification = (Notification)((Grid)sender).DataContext;
            ((Window.Current.Content as Frame)?.Content as MainPage)?.contentFrame.Navigate(typeof(CompanyDetailsPage), notification.CompanyId);
        }
    }
}
