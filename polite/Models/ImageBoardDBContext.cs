using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace polite.Models
{
    public class ImageBoardDBContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<BThread> BThreads { get; set; }
        public DbSet<TComment> TComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}