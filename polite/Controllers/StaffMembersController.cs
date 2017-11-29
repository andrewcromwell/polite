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
    public class StaffMembersController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: StaffMembers
        public ActionResult Index()
        {
            return View(db.StaffMembers.ToList());
        }

        // GET: StaffMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            return View(staffMember);
        }

        // GET: StaffMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,username,password,salt,type,boards,addedOn,lastActive")] StaffMember staffMember)
        {
            if (ModelState.IsValid)
            {
                db.StaffMembers.Add(staffMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffMember);
        }

        // GET: StaffMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            return View(staffMember);
        }

        // POST: StaffMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,username,password,salt,type,boards,addedOn,lastActive")] StaffMember staffMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffMember);
        }

        // GET: StaffMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffMember staffMember = db.StaffMembers.Find(id);
            if (staffMember == null)
            {
                return HttpNotFound();
            }
            return View(staffMember);
        }

        // POST: StaffMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffMember staffMember = db.StaffMembers.Find(id);
            db.StaffMembers.Remove(staffMember);
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
