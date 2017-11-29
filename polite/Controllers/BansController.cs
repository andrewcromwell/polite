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
    public class BansController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: Bans
        public ActionResult Index()
        {
            return View(db.Bans.ToList());
        }

        // GET: Bans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bans.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban);
        }

        // GET: Bans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,type,expired,allowRead,IP,ipmd5,global,boards,by,at,until,reason,staffnote,appeal,appealAt")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                db.Bans.Add(ban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ban);
        }

        // GET: Bans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bans.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban);
        }

        // POST: Bans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,type,expired,allowRead,IP,ipmd5,global,boards,by,at,until,reason,staffnote,appeal,appealAt")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ban);
        }

        // GET: Bans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bans.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban);
        }

        // POST: Bans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ban ban = db.Bans.Find(id);
            db.Bans.Remove(ban);
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
