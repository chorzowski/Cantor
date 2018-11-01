using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExchangeApplication.Utilities
{
    public class SaveData : Controller, ISaveData
    {
    

        public async Task<ActionResult> Save(int? param1, ApplicationDbContext db)
        {
            Info info = db.Infoes.Find(param1);
            if (info == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.Entry(info).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View("Home",info);
        }
    }
}