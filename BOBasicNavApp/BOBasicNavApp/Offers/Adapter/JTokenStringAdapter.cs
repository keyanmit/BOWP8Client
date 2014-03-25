using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BOBasicNavApp.Offers.Adapter
{
    public class JTokenStringAdapter
    {
        public static string GetStringFromJtoken(JToken token, string fallBack = "")
        {
            return token != null ? token.ToString() : fallBack;
        }
    }
}
