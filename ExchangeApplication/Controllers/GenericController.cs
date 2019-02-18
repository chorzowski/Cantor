using ExchangeApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExchangeApplication.Controllers

{
    public class GenericController : Controller
    {
        private GenericUnitOfWork uow = null;
        public GenericController()
        {
            uow = new GenericUnitOfWork();
        }
        public GenericController(GenericUnitOfWork _uow)
        {
            this.uow = _uow;
        }
        // GET: GenericPerson
        public ActionResult Index()
        {
            return View(uow.Repository<Info>().GetOverview().ToList().FirstOrDefault());
        }
        public ActionResult Details(int id)
        {
            Info user = uow.Repository<Info>().GetDetail(p => p.InfoId == id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Info info)
        {
            if (ModelState.IsValid)
            {
                uow.Repository<Info>().Add(info);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(info);
        }
        public ActionResult Delete(int id)
        {
            Info info = uow.Repository<Info>().GetDetail(p => p.InfoId == id);
            if (info == null)
                return HttpNotFound();
            return View(info);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Info info = uow.Repository<Info>().GetDetail(p => p.InfoId == id);
            uow.Repository<Info>().Delete(info);
            uow.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}