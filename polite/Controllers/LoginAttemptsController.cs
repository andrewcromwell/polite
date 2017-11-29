using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using polite.Models;

namespace polite.Controllers
{
    public class LoginAttemptsController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: LoginAttempts
        public ActionResult Index()
        {
            return View(db.LoginAttempts.ToList());
        }

        // GET: LoginAttempts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginAttempt loginAttempt = db.LoginAttempts.Find(id);
            if (loginAttempt == null)
            {
                return HttpNotFound();
            }
            return View(loginAttempt);
        }

        // GET: LoginAttempts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginAttempts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,username,IP,timestamp")] LoginAttempt loginAttempt)
        {
            if (ModelState.IsValid)
            {
                db.LoginAttempts.Add(loginAttempt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginAttempt);
        }

        // GET: LoginAttempts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginAttempt loginAttempt = db.LoginAttempts.Find(id);
            if (loginAttempt == null)
            {
                return HttpNotFound();
            }
            return View(loginAttempt);
        }

        // POST: LoginAttempts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,username,IP,timestamp")] LoginAttempt loginAttempt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginAttempt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginAttempt);
        }

        // GET: LoginAttempts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginAttempt loginAttempt = db.LoginAttempts.Find(id);
            if (loginAttempt == null)
            {
                return HttpNotFound();
            }
            return View(loginAttempt);
        }

        // POST: LoginAttempts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginAttempt loginAttempt = db.LoginAttempts.Find(id);
            db.LoginAttempts.Remove(loginAttempt);
            db.SaveChanges();
            return RedirectToAction("Index");
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
