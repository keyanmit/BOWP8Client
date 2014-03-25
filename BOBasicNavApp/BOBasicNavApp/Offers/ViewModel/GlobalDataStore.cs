using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOBasicNavApp.Offers.Model;

namespace BOBasicNavApp.Offers.ViewModel
{
    class GlobalDataStore
    {
        public static GeoLocation Location { get; set; }

        static GlobalDataStore()
        {
            Location=new GeoLocation();
        }
    }
}
