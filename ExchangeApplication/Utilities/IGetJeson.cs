using ExchangeApplication.Models;
using RestSharp;
using System.Collections.Generic;

namespace ExchangeApplication.Utilities
{
    public interface IGetJeson
    {
       List<AccountResponse> GetJson();
    }
}