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

        // GET: Edit
        [Authorize]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();

            var us = db.Users.Find(id);

            return View(us);
        }

        // GET: Edit/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = await db.Infoes.FindAsync(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // GET: Edit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InfoId,FirstName,LastName,Address,TelephoneNumber,USD,EUR,CHF,RUB,CZK,GBP,PLN")] Info info)
        {
            if (ModelState.IsValid)
            {
                db.Infoes.Add(info);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(info);
        }

        // GET: Edit/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = await db.Infoes.FindAsync(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info info = await db.Infoes.FindAsync(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            return View(info);
        }

        // POST: Edit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
         //   ApplicationUser u = db.Users.Find(id);
        //    var idd = User.Identity.GetUserId();
        //    db.Users.Remove(u.Id);
            Info info = await db.Infoes.FindAsync(id);
            db.Infoes.Remove(info);

            await db.SaveChangesAsync();
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
