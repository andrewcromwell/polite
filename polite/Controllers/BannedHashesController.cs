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
    public class BannedHashesController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: BannedHashes
        public ActionResult Index()
        {
            return View(db.BannedHashes.ToList());
        }

        // GET: BannedHashes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedHash bannedHash = db.BannedHashes.Find(id);
            if (bannedHash == null)
            {
                return HttpNotFound();
            }
            return View(bannedHash);
        }

        // GET: BannedHashes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BannedHashes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,md5,banTime,description")] BannedHash bannedHash)
        {
            if (ModelState.IsValid)
            {
                db.BannedHashes.Add(bannedHash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bannedHash);
        }

        // GET: BannedHashes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedHash bannedHash = db.BannedHashes.Find(id);
            if (bannedHash == null)
            {
                return HttpNotFound();
            }
            return View(bannedHash);
        }

        // POST: BannedHashes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,md5,banTime,description")] BannedHash bannedHash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bannedHash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bannedHash);
        }

        // GET: BannedHashes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannedHash bannedHash = db.BannedHashes.Find(id);
            if (bannedHash == null)
            {
                return HttpNotFound();
            }
            return View(bannedHash);
        }

        // POST: BannedHashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BannedHash bannedHash = db.BannedHashes.Find(id);
            db.BannedHashes.Remove(bannedHash);
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
