using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class BannedHash
    {
        public int ID { get; set; }
        public string md5 { get; set; }
        public DateTime banTime { get; set; }
        public string description { get; set; }
    }
}