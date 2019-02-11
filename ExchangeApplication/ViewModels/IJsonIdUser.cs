using System.Collections.Generic;
using ExchangeApplication.Models;

namespace ExchangeApplication.ViewModels
{
    public interface IJsonIdUser
    {
        List<AccountResponse> ar { get; set; }
        ApplicationUser user { get; set; }
    }
}