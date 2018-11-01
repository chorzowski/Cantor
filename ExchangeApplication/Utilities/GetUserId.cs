using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Script.Serialization;
using System.Data.Entity;
using System.Threading.Tasks;
using ExchangeApplication.Utilities;

namespace ExchangeApplication.Utilities
{
    public class GetUserId : IGetUserId
    {
   //     var id = User.Identity.GetUserId();

        public ApplicationUser GetUserID(ApplicationDbContext db)
        {
            var id = System.Web.HttpContext.Current.User.Identity.GetUserId();//db.User.Identity.GetUserId();
            var us = db.Users.Find(id);
            return us;
        }
    }
}