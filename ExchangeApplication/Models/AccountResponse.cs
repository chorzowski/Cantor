using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeApplication.Models
{
    public class AccountResponse
    {
        public DateTime publicationDate { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string code { get; set; }
        public int unit { get; set; }
        public float purchasePrice { get; set; }
        public float sellPrice { get; set; }
        public float averagePrice { get; set; }
    }
}