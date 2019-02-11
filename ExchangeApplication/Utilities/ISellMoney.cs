using System.Web.Mvc;
using ExchangeApplication.Models;

namespace ExchangeApplication.Utilities
{
    public interface ISellMoney
    {
        ActionResult SellEUR(int? numberIdUser, string currencyNameString, int amount, ApplicationDbContext db);
    }
}