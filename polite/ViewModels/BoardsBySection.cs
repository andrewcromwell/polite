using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using polite.Models;

namespace polite.ViewModels
{
    public class BoardsBySection
    {
        public string sectionName;
        public List<Board> boards;
    }
}