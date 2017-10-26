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
    public class BThreadsController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: BThreads
        public ActionResult Index()
        {
            var bThreads = db.BThreads.Include(b => b.Board);
            return View(bThreads.ToList());
        }

        // GET: BThreads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BThread bThread = db.BThreads.Find(id);
            if (bThread == null)
            {
                return HttpNotFound();
            }
            return View(bThread);
        }

        // GET: BThreads/Create
        public ActionResult Create()
        {
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName");
            return View();
        }

        // POST: BThreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BoardID,comment,subject,email,name,timestamp,threadID,ipAddress")] BThread bThread)
        {
            if (ModelState.IsValid)
            {
                db.BThreads.Add(bThread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName", bThread.BoardID);
            return View(bThread);
        }

        // GET: BThreads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BThread bThread = db.BThreads.Find(id);
            if (bThread == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName", bThread.BoardID);
            return View(bThread);
        }

        // POST: BThreads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BoardID,comment,subject,email,name,timestamp,threadID,ipAddress")] BThread bThread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bThread).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName", bThread.BoardID);
            return View(bThread);
        }

        // GET: BThreads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BThread bThread = db.BThreads.Find(id);
            if (bThread == null)
            {
                return HttpNotFound();
            }
            return View(bThread);
        }

        // POST: BThreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BThread bThread = db.BThreads.Find(id);
            db.BThreads.Remove(bThread);
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
