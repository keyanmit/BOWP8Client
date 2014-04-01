using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BOBasicNavApp.Offers.Facade;
using BOBasicNavApp.Offers.Model;

namespace BOBasicNavApp.Offers.ViewModel
{
    public class DailyDealsViewModel
    {
        public ObservableCollection<DealTileModel> DealsList { get; set; }

        public DailyDealsViewModel()
        {
            DealsList=new ObservableCollection<DealTileModel>();
            this.GetDealsForUserCurrentLocation();
        }

        private GeoLocation deviceLocation;

        public void GetDealsForUserCurrentLocation()
        {
            var locationManager = new GeoLocationFacade();
            locationManager.LocationLoadSuccess += LocationManagerOnLocationLoadSuccess;
            locationManager.GetDeviceLocationAsync();
        }

        public void RefreshDealsForCurrentLocation()
        {
            if (deviceLocation != null)
            {
                var DSManager = new DealServerFacade();                
                DSManager.DealsDownloadSuccess += DsManagerOnDealsDownloadSuccess;
                DSManager.DownloadDealsAsync(new DealServerQueryModel()
                {
                    Count = 10,
                    Latitude = deviceLocation.Latitude,
                    Longitude = deviceLocation.Longitude,
                    Refinement = new QueryRefineModel()
                    {
                        Keyword = "",
                        Offset = 0,
                        Ranking = "",
                        Resultsperbiz = 1,
                        TimeOut = 10,
                        source = ""
                    }
                });
            }
        }

        private void LocationManagerOnLocationLoadSuccess(object sender, LocationEventArgs locationEventArgs)
        {
            var DSManager = new DealServerFacade();
            deviceLocation = locationEventArgs.Location;
            DSManager.DealsDownloadSuccess += DsManagerOnDealsDownloadSuccess;
            DSManager.DownloadDealsAsync(new DealServerQueryModel()
            {
                Count = 10,
                Latitude = deviceLocation.Latitude,
                Longitude = deviceLocation.Longitude,
                Refinement = new QueryRefineModel()
                {
                    Keyword = "",
                    Offset = 0,
                    Ranking = "",
                    Resultsperbiz = 1,
                    TimeOut = 10,
                    source = ""
                }
            });
        }

        private void DsManagerOnDealsDownloadSuccess(object sender, DealListEventArgs eventArgs)
        {
            var deals = eventArgs.DealsList;
            foreach (DealTileModel deal in deals)
                DealsList.Add(deal);
        }

        public void LoadStaticData()
        {
            DealsList.Add(new DealTileModel() { BusinessName = "KFC Chicken", City = "Bangalore", CityVisibility = Visibility.Visible, DealImageUrl = "http://az389013.vo.msecnd.net/64c01f6f-cffe-4541-fef0-bfd4bf83f58b?v=5049664881&size=10&crop=true",DealInfo="$56 off in combo chicken", DealUrl="/",VoucherDiscountPercent="23",VoucherValue = "50",VoucherDiscountPercentVisibility = Visibility.Visible,VoucherValueVisibility = Visibility.Collapsed});
        }
    }
}
