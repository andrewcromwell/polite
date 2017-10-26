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
        public String shortName { get; set; }
        public String longName { get; set; }
        public String description { get; set; }
        public int maxPostId { get; set; }

        public virtual ICollection<BThread> threads { get; set; }
    }

}