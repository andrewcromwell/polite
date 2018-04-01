using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace polite.Models
{
    public class Post
    {
        [Key]
        [Column(Order=1)]
        public int ID { get; set; }
        [Key]
        [Column(Order=2)]
        public int BoardID { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public string tripcode { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string password { get; set; }
        public string file { get; set; }
        public string filemd5 { get; set; }
        public string fileType { get; set; }
        public string fileOriginal { get; set; }
        public int? fileSize { get; set; }
        public short? imageW { get; set; }
        public short? imageH { get; set; }
        public short? thumbW { get; set; }
        public short? thumbH { get; set; }
        public string ip { get; set; }
        public string ipmd5 { get; set; }
        public DateTime timestamp { get; set; }
        public bool stickied { get; set; }
        public bool locked { get; set; }
        public DateTime? deletedTimestamp { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? bumped { get; set; }

        public virtual Post Parent { get; set; }
        public virtual ICollection<Post> Children { get; set; }
        public virtual Board Board { get; set; }
    }
}