using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOBasicNavApp.Offers.Facade;
using BOBasicNavApp.Offers.Model;

namespace BOBasicNavApp.Offers.ViewModel
{
    public class DailyDealsViewModel
    {
        public ObservableCollection<DealTileModel> DealsList { get; set; }
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
    }
}
