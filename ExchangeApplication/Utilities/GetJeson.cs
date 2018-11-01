using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ExchangeApplication.Utilities
{
    public class GetJeson : IGetJeson
    {
        public AccountResponse GetJson()
        {
            string json = new WebClient().DownloadString("http://webtask.future-processing.com:8068/currencies/?application/json");
            var ar = new JavaScriptSerializer().Deserialize<AccountResponse>(json);
            return ar;
        }
    }
}