using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class Ban
    {
        public int ID { get; set; }
        public int type { get; set; }
        public bool expired { get; set; }
        public bool allowRead { get; set; }
        public string IP { get; set; }
        public string ipmd5 { get; set; }
        public bool global { get; set; }
        public string boards { get; set; }
        public string by { get; set; }
        public DateTime at { get; set; }
        public DateTime until { get; set; }
        public string reason { get; set; }
        public string staffnote { get; set; }
        public string appeal { get; set; }
        public DateTime appealAt { get; set; }
    }
}