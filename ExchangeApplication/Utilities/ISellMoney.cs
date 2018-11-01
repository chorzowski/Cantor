using System.Web.Mvc;
using ExchangeApplication.Models;

namespace ExchangeApplication.Utilities
{
    public interface ISellMoney
    {
        ActionResult SellEUR(int? numberIdUser, int amount, string currencyName, ApplicationDbContext db);
    }
}