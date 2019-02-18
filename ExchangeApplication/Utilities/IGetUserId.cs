using System.Web;
using ExchangeApplication.Models;

namespace ExchangeApplication.Utilities
{
    public interface IGetUserId
    {
        ApplicationUser GetUserID();
    }
}