using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace polite.Models
{
    public class Section
    {
        public int ID { get; set; }
        public int order { get; set; }
        public bool hidden { get; set; }
        public String name { get; set; }
        public String abbreviation { get; set; }


        public virtual ICollection<Board> Boards { get; set; }
    }

}