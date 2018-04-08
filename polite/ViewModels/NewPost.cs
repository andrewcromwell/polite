using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.ViewModels
{
    public class NewPost
    {
        public int resto { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string comment { get; set; }
        public string password { get; set; }
        public HttpPostedFile postFile { get; set; }
    }
}