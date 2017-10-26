using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class TComment
    {
        public int ID { get; set; }
        [StringLength(500)]
        public String comment { get; set; }
        [StringLength(50)]
        public String subject { get; set; }
        [StringLength(50)]
        public String email { get; set; }
        [StringLength(50)]
        public String name { get; set; }
        public DateTime timestamp { get; set; }
        public int commentId { get; set; }
        public String ipAddress { get; set; }
        
        public int BThreadId { get; set; }
        
        public virtual BThread BThread { get; set; }
    }
}