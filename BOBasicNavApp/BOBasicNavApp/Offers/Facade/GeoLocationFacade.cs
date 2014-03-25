using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using BOBasicNavApp.Offers.Adapter;
using BOBasicNavApp.Offers.Model;
using BOBasicNavApp.Offers.Settings;
using BOBasicNavApp.Offers.ViewModel;

namespace BOBasicNavApp.Offers.Facade
{
    public class LocationEventArgs
    {
        private GeoLocation location;

        public LocationEventArgs(GeoLocation _location)
        {
            location = _location;
        }

        public GeoLocation Location
        {
            get { return location; }
            set { location = value; }
        }
    }

    public class GeoLocationFacade
    {
        public static Geolocator Navigator;
        private GeoLocation DeviceLocation;

        public delegate void GeoLocationEventHandler(object sender, LocationEventArgs locationEventArgs);
        public event GeoLocationEventHandler LocationLoadSuccess;
        public event GeoLocationEventHandler LocationLoadFailed;

        static GeoLocationFacade()
        {
            Navigator = new Geolocator();
            Navigator.DesiredAccuracyInMeters = NavigatorSettings.NavigatorAccuracy;
        }

        public void GetDeviceLocationAsync()
        {
            Navigator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(NavigatorSettings.NavigatorWaitTime),
                timeout: TimeSpan.FromMinutes(NavigatorSettings.NavigatorAccuracy)).Completed += GeoLocationCompleted;
        }

        private void GeoLocationCompleted(IAsyncOperation<Geoposition> asyncInfo, AsyncStatus asyncStatus)
        {
            if (asyncStatus == AsyncStatus.Completed)
            {
                var deviceLocation = (Geoposition)asyncInfo.GetResults();
                try
                {
                    DeviceLocation = GeoLocationAdapter.GetLocationFromDeviceGeoPositionObject(deviceLocation);
                }
                catch (Exception ex)
                {
                    DeviceLocation = GeoLocationAdapter.GetDefaultLocaiton();
                }
                OnLocationLoadSuccess(new LocationEventArgs(DeviceLocation));
            }
        }


        #region events raised here

        protected virtual void OnLocationLoadSuccess(LocationEventArgs eventArgs)
        {
            GeoLocationEventHandler handler = LocationLoadSuccess;
            if (handler != null)
            {
                eventArgs.Location = DeviceLocation;
                handler(this, eventArgs);
            }
        }

        protected virtual void OnLocationLoadFailed(LocationEventArgs eventArgs)
        {
            GeoLocationEventHandler handler = LocationLoadFailed;
            if (handler != null)
            {
                eventArgs.Location = DeviceLocation;
                handler(this, eventArgs);
            }
        }
        #endregion
    }
}
