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
using WindowsAppEngG01.Utils;
using WindowsAppEngG01.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsAppEngG01.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPromotionPage : Page
    {
        public EditPromotionViewModel ViewModel = new EditPromotionViewModel();

        public EditPromotionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var passThrough = (EditPromotionPassThroughElement)e.Parameter;
            ViewModel.Promotion = CompanyManager.FindPromotion(passThrough.CompanyId, passThrough.PromotionId, passThrough.Identifier);
            ViewModel.CompanyId = passThrough.CompanyId;
            ViewModel.Identifier = passThrough.Identifier;
            this.DataContext = ViewModel;
        }
    }
}
