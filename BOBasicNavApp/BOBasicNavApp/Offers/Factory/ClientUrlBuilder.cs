using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BOBasicNavApp.Offers.Model;
using BOBasicNavApp.Offers.Settings;

namespace BOBasicNavApp.Offers.Factory
{
    public class ClientUrlBuilder
    {
        private static string AddClientIdToUri(string url)
        {

            if (url.IndexOf('?') == -1)
                url += '?';
            if (url[url.Length - 1] != '?')
                url += "&";
            url += ("bor=" + DealServerSettings.ClientId);
            return url;
        }

        public static string FixRedirectUrl(string url, string dealId)
        {
            if (!url.Contains("/offers/"))
            {
                /* expects the seo url to be of form "/offers/<seo>"
                 * "/offers/" will be a minimum requirement for a potentially valid seo url
                 * if in-valid use deal id to redirect the user
                 * if a in-valid url escapes this check, user will be redirected to bing offers atleast
                 */
                url = DealServerSettings.BingOffersDealDetailsPath + dealId;
            }
            return url;
        }

        public static Uri BuildUri(string sitePath)
        {
            return new Uri(AddClientIdToUri(DealServerSettings.BingDomain + sitePath));
        }

        public static Uri GetDealServerUri(DealServerQueryModel model)
        {
            var UriString = String.Format(DealServerSettings.BingOffersNearbyDeals, DealServerSettings.DealsDomain,
                model.Latitude,
                model.Longitude, model.Count, DealServerSettings.QueryRadius, DealServerSettings.ClientId, model.Refinement.ConstructRefinementParam());
            return new Uri(UriString);
        }
    }
}
