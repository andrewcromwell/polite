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
    public class TCommentsController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: TComments
        public ActionResult Index()
        {
            var tComments = db.TComments.Include(t => t.BThread);
            return View(tComments.ToList());
        }

        // GET: TComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TComment tComment = db.TComments.Find(id);
            if (tComment == null)
            {
                return HttpNotFound();
            }
            return View(tComment);
        }

        // GET: TComments/Create
        public ActionResult Create()
        {
            ViewBag.BThreadId = new SelectList(db.BThreads, "ID", "comment");
            return View();
        }

        // POST: TComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,comment,subject,email,name,timestamp,commentId,ipAddress,BThreadId")] TComment tComment)
        {
            if (ModelState.IsValid)
            {
                db.TComments.Add(tComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BThreadId = new SelectList(db.BThreads, "ID", "comment", tComment.BThreadId);
            return View(tComment);
        }

        // GET: TComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TComment tComment = db.TComments.Find(id);
            if (tComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BThreadId = new SelectList(db.BThreads, "ID", "comment", tComment.BThreadId);
            return View(tComment);
        }

        // POST: TComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,comment,subject,email,name,timestamp,commentId,ipAddress,BThreadId")] TComment tComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BThreadId = new SelectList(db.BThreads, "ID", "comment", tComment.BThreadId);
            return View(tComment);
        }

        // GET: TComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TComment tComment = db.TComments.Find(id);
            if (tComment == null)
            {
                return HttpNotFound();
            }
            return View(tComment);
        }

        // POST: TComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TComment tComment = db.TComments.Find(id);
            db.TComments.Remove(tComment);
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
