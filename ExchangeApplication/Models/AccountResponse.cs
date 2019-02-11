using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeApplication.Models
{
    public class RootObject
    {
        public string currency { get; set; }
        public string code { get; set; }       
        public double bid { get; set; }
        public double ask { get; set; }
    }

    public class AccountResponse
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public List<RootObject> rates { get; set; }
    }
}