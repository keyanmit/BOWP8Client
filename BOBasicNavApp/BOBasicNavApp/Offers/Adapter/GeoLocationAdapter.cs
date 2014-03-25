using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using BOBasicNavApp.Offers.Model;

namespace BOBasicNavApp.Offers.Adapter
{
    class GeoLocationAdapter
    {
        public static GeoLocation GetLocationFromDeviceGeoPositionObject(Geoposition location)
        {
            return location.Coordinate == null ? new GeoLocation() : new GeoLocation()
            {
                Latitude = location.Coordinate == null ? -12 : location.Coordinate.Latitude,
                Longitude = location.Coordinate == null? -24 : location.Coordinate.Longitude,
                City = location.CivicAddress ==null ? "":location.CivicAddress.City
            };
        }

        public static GeoLocation GetDefaultLocaiton()
        {
            return new GeoLocation()
            {
                City = "Bangalore",
                Latitude = 12.75,
                Longitude = 74.55
            };
        }
    }
}
