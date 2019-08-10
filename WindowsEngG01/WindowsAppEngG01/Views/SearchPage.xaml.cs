﻿using System;
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
    public sealed partial class SearchPage : Page
    {
        public SearchViewModel ViewModel = new SearchViewModel();
        public SearchPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void LvSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LvSearchResultsItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }
    }
}
