using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class TComment
    {
        public int ID { get; set; }
        public String comment { get; set; }
        public String subject { get; set; }
        public String email { get; set; }
        public String name { get; set; }
        public DateTime timestamp { get; set; }
        public int commentId { get; set; }
        public String ipAddress { get; set; }
        
        public int BThreadId { get; set; }
        
        public virtual BThread BThread { get; set; }
    }
}