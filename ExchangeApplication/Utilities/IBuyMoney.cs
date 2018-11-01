﻿using System.Web.Mvc;
using ExchangeApplication.Models;

namespace ExchangeApplication.Utilities
{
    public interface IBuyMoney
    {
        ActionResult BuyEUR(int? numberIdUser, int amount, string currencyName, ApplicationDbContext db);
    }
}