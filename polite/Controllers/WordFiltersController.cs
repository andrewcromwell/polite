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
    public class WordFiltersController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: WordFilters
        public ActionResult Index()
        {
            return View(db.WordFilters.ToList());
        }

        // GET: WordFilters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WordFilter wordFilter = db.WordFilters.Find(id);
            if (wordFilter == null)
            {
                return HttpNotFound();
            }
            return View(wordFilter);
        }

        // GET: WordFilters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WordFilters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,word,replacedBy,boards,created,regex")] WordFilter wordFilter)
        {
            if (ModelState.IsValid)
            {
                db.WordFilters.Add(wordFilter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wordFilter);
        }

        // GET: WordFilters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WordFilter wordFilter = db.WordFilters.Find(id);
            if (wordFilter == null)
            {
                return HttpNotFound();
            }
            return View(wordFilter);
        }

        // POST: WordFilters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,word,replacedBy,boards,created,regex")] WordFilter wordFilter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wordFilter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wordFilter);
        }

        // GET: WordFilters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WordFilter wordFilter = db.WordFilters.Find(id);
            if (wordFilter == null)
            {
                return HttpNotFound();
            }
            return View(wordFilter);
        }

        // POST: WordFilters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WordFilter wordFilter = db.WordFilters.Find(id);
            db.WordFilters.Remove(wordFilter);
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
