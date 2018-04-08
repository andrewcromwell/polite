﻿using System;
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

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Board).Include(p => p.Parent);
            return View(posts.ToList());
        }

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

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
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

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName");
            ViewBag.parentId = new SelectList(db.Posts, "ID", "name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BoardID,parentId,name,tripcode,email,subject,message,password,file,filemd5,fileType,fileOriginal,fileSize,imageW,imageH,thumbW,thumbH,ip,ipmd5,timestamp,stickied,locked,deletedTimestamp,isDeleted,bumped")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName", post.BoardID);
            ViewBag.parentId = new SelectList(db.Posts, "ID", "name", post.parentId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName", post.BoardID);
            ViewBag.parentId = new SelectList(db.Posts, "ID", "name", post.parentId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BoardID,parentId,name,tripcode,email,subject,message,password,file,filemd5,fileType,fileOriginal,fileSize,imageW,imageH,thumbW,thumbH,ip,ipmd5,timestamp,stickied,locked,deletedTimestamp,isDeleted,bumped")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BoardID = new SelectList(db.Boards, "ID", "shortName", post.BoardID);
            ViewBag.parentId = new SelectList(db.Posts, "ID", "name", post.parentId);
            return View(post);
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
