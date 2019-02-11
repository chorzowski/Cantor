using ExchangeApplication.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ExchangeApplication.Utilities
{
    public class GetJeson : IGetJeson
    {
        const string BaseUrl = "http://api.nbp.pl";//c/?format=json";

        readonly IRestClient _client;
        //    public AccountResponse GetJson()
        //  {
        //         string json = new WebClient().DownloadString("http://webtask.future-processing.com:8068/currencies/?application/json?application/json");
        //         var ar = new JavaScriptSerializer().Deserialize<AccountResponse>(json);

        public GetJeson()
        {
            _client = new RestClient(BaseUrl);
        }

        public List<AccountResponse> GetJson()
        {
            var request = new RestRequest("api/exchangerates/tables/c/?format=json", Method.GET) { RequestFormat = DataFormat.Json };

            var response = _client.Execute<List<AccountResponse>>(request);

            if (response.Data == null)
                throw new Exception(response.ErrorMessage);

            Debug.WriteLine(response.Data.ToString());

            return response.Data;
        }

        //public AccountResponse GetJson()
        //{
        //    //var client = new RestClient("http://webtask.future-processing.com:8068/currencies/");
        //    //var request = new RestRequest("?application/json", Method.GET) { RequestFormat = DataFormat.Json };
        //    //var ar = client.Execute<AccountResponse>(request).Data;








        //    var client = new RestClient("http://webtask.future-processing.com:8068/currencies/");

        //    var request = new RestRequest("?application/json");

        //    var response = client.Get(request);

        //    Console.WriteLine("Drukuj cokolwiek " + ar.items[0].name);

        //    return ar;
        //}
        // }
    }
}