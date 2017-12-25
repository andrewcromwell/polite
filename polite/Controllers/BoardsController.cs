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
    public class BoardsController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: Boards
        public ActionResult Index()
        {
            var boards = db.Boards.Include(b => b.Section);
            return View(boards.ToList());
        }

        // GET: Boards/Details/5
        [Route("{shortName}")]
        public ActionResult PostsByBoard(string shortName)
        {
            if (shortName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Where(b => b.shortName.Equals(shortName)).FirstOrDefault();
            if (board == null)
            {
                return HttpNotFound();
            }
            ViewBag.shortName = board.shortName;
            ViewBag.longName = board.longName;
            var posts = board.Posts.Where((p => p.parentId == null));
            return View(posts);
        }

        // GET: Boards/Details/5
        [Route("{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // GET: Boards/Create
        public ActionResult Create()
        {
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "name");
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,order,shortName,type,start,longName,description,sectionID,maxImageSize,maxPages,maxAge,markPage,maxReplies,messageLength,createdOn,locked,includeHeader,redirectToThread,anonymous,forcedAnon,trial,popular,defaultStyle,useIdentities,maxPostId")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.Boards.Add(board);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sectionID = new SelectList(db.Sections, "ID", "name", board.sectionID);
            return View(board);
        }

        // GET: Boards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "name", board.sectionID);
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,order,shortName,type,start,longName,description,sectionID,maxImageSize,maxPages,maxAge,markPage,maxReplies,messageLength,createdOn,locked,includeHeader,redirectToThread,anonymous,forcedAnon,trial,popular,defaultStyle,useIdentities,maxPostId")] Board board)
        {
            if (ModelState.IsValid)
            {
                db.Entry(board).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "name", board.sectionID);
            return View(board);
        }

        // GET: Boards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Board board = db.Boards.Find(id);
            if (board == null)
            {
                return HttpNotFound();
            }
            return View(board);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Board board = db.Boards.Find(id);
            db.Boards.Remove(board);
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
