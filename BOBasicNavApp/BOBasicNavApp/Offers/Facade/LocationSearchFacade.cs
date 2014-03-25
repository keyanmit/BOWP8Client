using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BOBasicNavApp.Offers.Model;
using BOBasicNavApp.Offers.Settings;

namespace BOBasicNavApp.Offers.Facade
{
    class LocationSearchFacade
    {
        private string BuildBingQueryString(string query)
        {
            var queryModel = new BingMapsQueryModel(
                Query: HttpUtility.UrlEncode(query) );

            return BingMapsSettings.BingMapsDomain + "?" + queryModel.BuildBingQueryString();
        }        
    }
}
