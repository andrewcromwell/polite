using polite.Models;
using polite.ViewModels;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System.IO;
using System.Drawing;
using System.Web.Mvc;
using polite.Helpers;

namespace polite.Services
{
    public class PostService
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

        public enum PostResult
        {
            PostFailure,
            NewThreadCreated,
            NewReplyCreated
        };

        // get all posts.
        public ICollection<Post> GetPost()
        {
            var posts = db.Posts.Include(p => p.Board);
            return posts.ToList();
        }

        // get the OP, which includes children.
        public Post GetPostByBoardAndID(string shortName, int id)
        {
            Post post = db.Posts
                .Where(p => p.Board.shortName.Equals(shortName) &&
                       p.ID == id)
                .FirstOrDefault();
            return post;
        }

        public void SaveFileToBoardFolder(string shortName,
            HttpPostedFileBase postFile,
            double time,
            string uploadName)
        {
            long iTime = Convert.ToInt64(time * 1000);
            string fileExt = Path.GetExtension(uploadName);
            string uploadsFolder = HttpContext.Current.Server
                .MapPath("~/Content/Images");
            System.IO.Directory.CreateDirectory(uploadsFolder);
            string folder = Path.Combine(uploadsFolder, shortName);
            System.IO.Directory.CreateDirectory(folder);
            string justFile = iTime + fileExt;
            string fileName = Path.Combine(folder, justFile);
            postFile.SaveAs(fileName);
        }

        internal PostResult CreatePost(NewPost post,
            HttpPostedFileBase postFile,
            string shortName,
            ModelStateDictionary modelState,
            string IPAddress)
        {
            bool valid = modelState.IsValid;
            if (!valid)
                return PostResult.PostFailure;
            bool boardExists = db.Boards
                .Where(b => b.shortName.Equals(shortName))
                .Count() > 0;
            if (!boardExists)
                return PostResult.PostFailure;

            DateTime cur = DateTime.UtcNow;
            bool isBanned = db.Bans.Where(b => b.IP.Equals(IPAddress) &&
                                          b.until >= cur)
                                   .Count() > 0;
            if (isBanned)
                return PostResult.PostFailure;

            if (post.resto == 0)
                return MakeNewThread(post, postFile, shortName, IPAddress);
            else
                return MakeNewReply(post, postFile, shortName, IPAddress);
        }

        internal PostResult MakeNewThread(NewPost post,
            HttpPostedFileBase postFile,
            string shortName,
            string IPAddress)
        {
            bool imageExists = postFile != null;
            if (!imageExists)
                return PostResult.PostFailure;

            bool imageIsGood = ImageGood(postFile, shortName);
            string ip = IPAddress;
            DateTime cur = DateTime.UtcNow;
            if (imageIsGood)
            {
                double iCur = DateService.DateTimeToUnixTimestamp(cur);
                SaveFileToBoardFolder(shortName, postFile, iCur, postFile.FileName);
                saveSmallFileToBoardFolder(shortName,
                    postFile, iCur, postFile.FileName);
                int fsize = postFile.ContentLength;
                savePostToDatabase(post, postFile, shortName, cur);
                return PostResult.NewThreadCreated;
            }

            return PostResult.PostFailure;
        }

        internal PostResult MakeNewReply(NewPost post,
            HttpPostedFileBase postFile,
            string shortName,
            string IPAddress)
        {
            bool imageExists = postFile != null;
            bool imageIsGood = !imageExists ||
                ImageGood(postFile, shortName);
            string ip = IPAddress;
            DateTime cur = DateTime.UtcNow;
            bool isParentValid = db.Posts.Where(p => p.ID == post.resto)
                                         .Count() > 0;
            if (imageIsGood && isParentValid)
            {
                double iCur = DateService.DateTimeToUnixTimestamp(cur);
                if (imageExists)
                {
                    SaveFileToBoardFolder(shortName, postFile, iCur, postFile.FileName);
                    saveSmallFileToBoardFolder(shortName,
                        postFile, iCur, postFile.FileName);
                    int fsize = postFile.ContentLength;
                }
                savePostToDatabase(post, postFile, shortName, cur);
                return PostResult.NewReplyCreated;
            }

            return PostResult.PostFailure;
        }

        internal bool ImageGood(HttpPostedFileBase postFile, string shortName)
        {
            return (HttpPostedFileBaseExtensions.IsImage(postFile) &&
                postFile.ContentLength > 0 &&
                db.Boards
                .Where(b => b.shortName.Equals(shortName))
                .First().maxImageSize > postFile.ContentLength);
        }


        public void saveSmallFileToBoardFolder(string shortName,
            HttpPostedFileBase postFile,
            double time,
            string uploadName)
        {
            long iTime = Convert.ToInt64(time * 1000);
            string uploadsFolder = HttpContext.Current.Server
                .MapPath("~/Content/Images");
            System.IO.Directory.CreateDirectory(uploadsFolder);
            string folder = Path.Combine(uploadsFolder, shortName);
            System.IO.Directory.CreateDirectory(folder);
            string justFile = iTime + "s.jpg";
            string fileName = Path.Combine(folder, justFile);
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            Size size = new Size(250, 0);
            using (FileStream outStream = new FileStream(fileName, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(postFile.InputStream)
                                .Resize(size)
                                .Format(format)
                                .Save(outStream);
                }
            }
        }

        public void savePostToDatabase(NewPost post,
            HttpPostedFileBase postFile,
            string shortName,
            DateTime cur)
        {
            Post p = new Post();
            p.name = post.name;
            p.email = post.email;
            p.message = post.comment;
            var parent = db.Posts.Where(po => po.ID == post.resto);
            int parentCount = parent.Count();
            if (parentCount == 0)
            {
                p.Parent = null;
            }
            else
            {
                p.Parent = parent.First();
            }
            p.password = post.password;
            p.subject = post.subject;
            p.Board = db.Boards.Where(b => b.shortName.Equals(shortName))
                               .First();
            p.BoardID = p.Board.ID;
            p.stickied = false;
            p.locked = false;
            p.isDeleted = false;
            p.timestamp = cur;
            p.bumped = cur;
            if (postFile != null)
            {
                p.fileOriginal = postFile.FileName;
                p.fileType = Path.GetExtension(postFile.FileName);
                double dCur = DateService.DateTimeToUnixTimestamp(cur);
                long iCur = Convert.ToInt64(dCur * 1000);
                p.file = iCur.ToString();
                p.fileSize = postFile.ContentLength;
            }
            p.ID = p.Board.maxPostId + 1;
            p.Board.maxPostId++;
            p.Board.Posts.Add(p);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}