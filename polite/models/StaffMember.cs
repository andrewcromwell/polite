using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class StaffMember
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public int type { get; set; }
        public string boards { get; set; }
        public DateTime addedOn { get; set; }
        public DateTime lastActive { get; set; }
    }
}