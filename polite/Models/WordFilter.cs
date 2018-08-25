using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class WordFilter
    {
        public int ID { get; set; }
        public string word { get; set; }
        public string replacedBy { get; set; }
        public string boards { get; set; }
        public DateTime created { get; set; }
        public bool regex { get; set; }
    }
}