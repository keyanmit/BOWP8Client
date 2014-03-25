using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BOBasicNavApp.Offers.Model
{
    public class DealTileModel
    {
        public string BusinessName { get; set; }
        public string BusinessAddressLine1 { get; set; }        
        public string City { get; set; }
        public string DealInfo { get; set; }
        public string DealImageUrl { get; set; }
        public string DealUrl { get; set; }
        public string Country_Region { get; set; }
        /*
         * deal url will always be valid by the time its available to the app. it either has seo link or deal id link. 
         */
        public string VoucherValue  { get; set; }
        public string VoucherDiscountPercent { get; set; }
        //public string CurrencySymbol { get; set; }

        public Visibility VoucherValueVisibility { get; set; }
        public Visibility VoucherDiscountPercentVisibility { get; set; }
        public Visibility CityVisibility { get; set; }
    }
}
