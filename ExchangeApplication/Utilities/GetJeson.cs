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
        
    }
}