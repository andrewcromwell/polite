using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using polite.Models;
using polite.ViewModels;

namespace polite.Controllers
{
    public class PostsController : Controller
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        // GET: Boards/Details/5
        [Route("{shortName}/thread/{id:int}")]
        public ActionResult Thread(string shortName, int? id)
        {
            if (shortName == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Where(p => p.Board.shortName.Equals(shortName) && p.ID == id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{shortName}/post")]
        public ActionResult CreateReply([Bind(Include = "resto,name,email,subject,comment,password")] NewPost post, HttpPostedFileBase postFile, string shortName)
        {
            throw new NotImplementedException();
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
