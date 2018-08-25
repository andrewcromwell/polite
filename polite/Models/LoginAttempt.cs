using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class LoginAttempt
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string IP { get; set; }
        public DateTime timestamp { get; set; }
    }
}