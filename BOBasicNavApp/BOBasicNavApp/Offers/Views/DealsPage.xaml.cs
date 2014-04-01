using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BOBasicNavApp.Offers.Facade;
using BOBasicNavApp.Offers.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BOBasicNavApp.Offers.Model;
using System.Windows.Media;

namespace BOBasicNavApp.Offers.Views
{
    public partial class DealsPage : PhoneApplicationPage
    {
        public DealsPage()
        {
            InitializeComponent();
            /*var vm = new DailyDealsViewModel();
            vm.LoadStaticData();
            DataContext = vm.DealsList;*/
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            var vm = new DailyDealsViewModel();            
            DataContext = vm;                 
            //keyanList.ItemsSource = vm.DealsList;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var img = (Image)sender;
            var dataContext = (DealTileModel)img.DataContext;
            img.Source=new System.Windows.Media.Imaging.BitmapImage(new Uri(dataContext.DealImageUrl));
            //img.Source = new Bitmap(new Uri(img.DataContext.DealImageUrl));
            
        }
    }
}