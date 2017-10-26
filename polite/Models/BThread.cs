using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class BThread
    {
        public int ID { get; set; }
        public int BoardID { get; set; }
        public String comment { get; set; }
        public String subject { get; set; }
        public String email { get; set; }
        public String name { get; set; }
        public DateTime timestamp { get; set; }
        public int threadID { get; set; }
        public String ipAddress { get; set; }

        public virtual Board Board { get; set; }
        public virtual ICollection<TComment> comments { get; set; }
    }
}