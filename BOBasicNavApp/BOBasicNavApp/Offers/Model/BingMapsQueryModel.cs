using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOBasicNavApp.Offers.Settings;

namespace BOBasicNavApp.Offers.Model
{
    class BingMapsQueryModel
    {
        public string query { get; set; }
        public string inclnb { get; set; } //include neighbourhood
        public string incl { get; set; }
        public string maxResults { get; set; }

        public BingMapsQueryModel()
        {
            query = inclnb = incl = maxResults = string.Empty;
        }

        public BingMapsQueryModel(string Query)
        {
            query = Query;
            inclnb = "1";
            incl = "queryParse,ciso";
            maxResults = "5";
        }

        public string BuildBingQueryString()
        {
            var queryString = string.Empty;
            if (!string.IsNullOrEmpty(query))
                queryString += "&query=" + query;
            if (!string.IsNullOrEmpty(incl))
                queryString += "&incl=" + incl;
            if (!string.IsNullOrEmpty(inclnb))
                queryString += "&inclnb=" + inclnb;
            if (!string.IsNullOrEmpty(maxResults))
                queryString += "&maxResults=" + maxResults;
            queryString += "&key=" + BingMapsSettings.ApiKey;


            return queryString.Substring(1);
        }
    }
}
