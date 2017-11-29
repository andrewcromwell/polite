using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace polite.Models
{
    public class Board
    {
        public int ID { get; set; }
        public int order { get; set; }
        public String shortName { get; set; }
        public int type { get; set; } // 0 for normal; 1 for text-only; etc.
        public int start { get; set; } // start posts at 1
        public String longName { get; set; }
        public String description { get; set; }
        public int sectionID { get; set; } // what section does it show up in?
        public int maxImageSize { get; set; }
        public int maxPages { get; set; }
        public int maxAge { get; set; }
        public int markPage { get; set; }
        public int maxReplies { get; set; }
        public int messageLength { get; set; }
        public DateTime createdOn { get; set; }
        public bool locked { get; set; }
        public string includeHeader { get; set; }
        public bool redirectToThread { get; set; }
        public string anonymous { get; set; }
        public bool forcedAnon { get; set; }
        public bool trial { get; set; }
        public bool popular { get; set; }
        public string defaultStyle { get; set; }
        public bool useIdentities { get; set; }
        public int maxPostId { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual Section Section { get; set; }
    }

}