using System.Web.Mvc;
using ExchangeApplication.Models;

namespace ExchangeApplication.Utilities
{
    public interface IBuyMoney
    {
        ActionResult BuyEUR(int? numberIdUser, string currencyNameString, int amount, ApplicationDbContext db);
    }
}