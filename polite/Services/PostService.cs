using polite.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace polite.Services
{
    public class PostService
    {
        private ImageBoardDBContext db = new ImageBoardDBContext();

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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}