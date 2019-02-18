using ExchangeApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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

        private GenericUnitOfWork uow = null;

        public SaveData()
        {
            uow = new GenericUnitOfWork();
        }

        public SaveData(GenericUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ActionResult Save(int? param1, ApplicationDbContext db)
        {
  
               Info info = db.Infoes.Find(param1);

            if (info == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                     db.Entry(info).State = EntityState.Modified;
           //        var cos = uow.Repository<ApplicationUser>.;// Entry(idLoggedUser).State = EntityState.Modified;
           //        context.Entry(entity).State = EntityState.Modified;

                   db.SaveChangesAsync();
           //      uow.SaveChanges(); nie wiem jak zasosowac tutaj uow... w sensie nie wiem
           //      jak dodac zmiany uow tak jak wyzej w EntityState.Modifited a potem to zapisac w bazie danych... 

                return RedirectToAction("Index","Home");
            }
            return View("Home",info);
        }
    }
}