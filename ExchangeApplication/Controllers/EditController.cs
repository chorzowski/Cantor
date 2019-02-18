using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExchangeApplication.Models;
using Microsoft.AspNet.Identity;

namespace ExchangeApplication.Controllers
{
    public class EditController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private GenericUnitOfWork uow = null;
        public EditController()
        {
            uow = new GenericUnitOfWork();
        }
        public EditController(GenericUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        // GET: Edit
        [Authorize]
        public ActionResult Index()
        {
            var idLoggedUser = User.Identity.GetUserId();

            var loggedUser = uow.Repository<ApplicationUser>().GetDetail(p => p.Id == idLoggedUser);

            return View(loggedUser);
        }

        // GET: Edit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var loggedUser = uow.Repository<Info>().GetDetail(p => p.InfoId == id); // add GetAwaiter
            if (loggedUser == null)
            {
                return HttpNotFound();
            }
            return View(loggedUser);
        }

        // GET: Edit/Create
        [ChildActionOnly]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ChildActionOnly]
        public ActionResult Create([Bind(Include = "InfoId,FirstName,LastName,Address,TelephoneNumber,USD,EUR,CHF,RUB,CZK,GBP,PLN")] Info info)
        {
            if (ModelState.IsValid)
            {
                uow.Repository<Info>().Add(info);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(info);
        }

        // GET: Edit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var loggedUser = uow.Repository<Info>().GetDetail(p => p.InfoId == id); // add GetAwaiter
            if (loggedUser == null)
            {
                return HttpNotFound();
            }
            return View(loggedUser);
        }

        // POST: Edit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "InfoId,FirstName,LastName,Address,TelephoneNumber,USD,EUR,CHF,RUB,CZK,GBP,PLN")] Info info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(info).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(info);
        }

        // GET: Edit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var loggedUser = uow.Repository<Info>().GetDetail(p => p.InfoId == id); // add GetAwaiter
            if (loggedUser == null)
            {
                return HttpNotFound();
            }
            return View(loggedUser);
        }

        // POST: Edit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   

            var idLoggedUser = User.Identity.GetUserId();

            var loggedUser = uow.Repository < ApplicationUser>().GetDetail(p => p.Id == idLoggedUser);
            uow.Repository<ApplicationUser>().Delete(loggedUser);

            uow.SaveChanges();
            return RedirectToAction("Log_Off", "Account");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
