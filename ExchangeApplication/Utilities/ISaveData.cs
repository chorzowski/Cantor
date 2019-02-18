using System.Threading.Tasks;
using System.Web.Mvc;
using ExchangeApplication.Models;

namespace ExchangeApplication.Utilities
{
    public interface ISaveData
    {
        ActionResult Save(int? param1, ApplicationDbContext db);
    }
}