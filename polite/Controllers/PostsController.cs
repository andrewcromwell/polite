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
using polite.Services;

namespace polite.Controllers
{
    public class PostsController : Controller
    {
        private PostService _service = new PostService();

        // GET: Boards/Details/5
        [Route("{shortName}/thread/{id:int}")]
        public ActionResult Thread(string shortName, int? id)
        {
            if (shortName == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _service.GetPostByBoardAndID(shortName, (int)id);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
