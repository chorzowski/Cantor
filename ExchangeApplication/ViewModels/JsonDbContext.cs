using ExchangeApplication.Models;
using ExchangeApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeApplication.ViewModels
{
    public class JsonIdUser : IJsonIdUser
    {
       
        public List<AccountResponse> ar { get; set; }
        
        public ApplicationUser user { get; set; }

  //      public string idUser;
    }

}