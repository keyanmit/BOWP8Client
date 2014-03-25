using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BOBasicNavApp.Offers.Adapter;
using BOBasicNavApp.Offers.Factory;
using BOBasicNavApp.Offers.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BOBasicNavApp.Offers.Facade
{
    public class DealListEventArgs : EventArgs
    {
        private List<DealTileModel> dealsList;

        public DealListEventArgs(List<DealTileModel> _dealsList)
        {
            dealsList = _dealsList;
        }

        public List<DealTileModel> DealsList
        {
            get { return dealsList; }
            set { dealsList = value; }
        }
    }

    public class DealServerFacade
    {
        private List<DealTileModel> DealList;

        public DealServerFacade()
        {
            DealList = new List<DealTileModel>();
        }

        public delegate void DealsDownloadcompleteEventHandler(object sender, DealListEventArgs eventArgs);

        public event DealsDownloadcompleteEventHandler DealsDownloadSuccess;
        public event DealsDownloadcompleteEventHandler DealsDownloadFailed;

        public void DownloadDealsAsync(DealServerQueryModel query)
        {
            var dlManager = new WebClient();
            dlManager.DownloadStringCompleted += DownloadCompleteCallBack;
            var url = ClientUrlBuilder.GetDealServerUri(query);
            dlManager.DownloadStringAsync(url);
        }

        private void DownloadCompleteCallBack(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    var response = (JArray)JsonConvert.DeserializeObject(e.Result);
                    var deal = response.Children()
                        .Cast<JObject>().ToList();

                    DealTileModel tmpDeal;
                    foreach (var item in deal)
                    {
                        #region deal json parsing
                        tmpDeal = new DealTileModel()
                        {
                            BusinessName = JTokenStringAdapter.GetStringFromJtoken(item["business"]["name"]),
                            City =
                                (item["business"]["locations"] != null && item["business"]["locations"].First != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["business"]["locations"][0]["city"])
                                    : "",
                            DealImageUrl =
                                (item["deal_images"][0] != null
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["deal_images"][0]["image_url"],
                                        "https://pbs.twimg.com/profile_images/440575400763080704/7cIzjF0F.jpeg")
                                    : ""), //Loaded="FrameworkElement_OnLoaded" image from twitter
                            DealInfo = JTokenStringAdapter.GetStringFromJtoken(item["title"]),
                            DealUrl =
                                ClientUrlBuilder.FixRedirectUrl(
                                    JTokenStringAdapter.GetStringFromJtoken(item["seo_url"]["seo_url_full_url"], "/offers"),
                                    JTokenStringAdapter.GetStringFromJtoken(item["id"])),
                            VoucherDiscountPercent =
                                (item["deal_info"] != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["deal_info"]["voucher_discount_percent"]) + "%"
                                    : "",
                            VoucherDiscountPercentVisibility =
                                (((item["deal_info"] != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["deal_info"]["voucher_discount_percent"])
                                    : "").Length > 0
                                    ? Visibility.Visible
                                    : Visibility.Collapsed),
                            VoucherValue =
                                (item["deal_info"] != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["currency_symbol"]) +
                                      JTokenStringAdapter.GetStringFromJtoken(item["deal_info"]["voucher_value"])
                                    : "",
                            VoucherValueVisibility =
                                (((item["deal_info"] != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["business"]["currency_symbol"]) +
                                      JTokenStringAdapter.GetStringFromJtoken(item["deal_info"]["voucher_value"])
                                    : "").Length > 2
                                    ? Visibility.Visible
                                    : Visibility.Collapsed),
                            CityVisibility =
                                ((item["business"]["locations"] != null && item["business"]["locations"].First != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["business"]["locations"][0]["city"])
                                    : "").Length > 1
                                    ? Visibility.Visible
                                    : Visibility.Collapsed,
                            Country_Region =
                                (item["business"]["locations"] != null && item["business"]["locations"].First != null)
                                    ? JTokenStringAdapter.GetStringFromJtoken(item["business"]["locations"][0]["country_region"])
                                    : ""
                        };
                        #endregion

                        if (tmpDeal.VoucherDiscountPercentVisibility == Visibility.Visible &&
                            tmpDeal.VoucherValueVisibility == Visibility.Visible)
                            tmpDeal.VoucherValueVisibility = Visibility.Collapsed;
                        DealList.Add(tmpDeal);
                    }
                }
                catch (Exception exception)
                {
                    //handle exception
                }
                OnDealsDownloadSuccess(new DealListEventArgs(DealList));
            }
            else
            {
                //handle
                OnDealsDownloadFailed(new DealListEventArgs(DealList));
            }
        }

        #region event raising definition

        protected virtual void OnDealsDownloadSuccess(DealListEventArgs eventArgs)
        {
            DealsDownloadcompleteEventHandler handler = DealsDownloadSuccess;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }

        protected virtual void OnDealsDownloadFailed(DealListEventArgs eventArgs)
        {
            DealsDownloadcompleteEventHandler handler = DealsDownloadFailed;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }
        #endregion
    }
}
