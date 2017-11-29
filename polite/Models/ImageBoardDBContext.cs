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
        public DbSet<Ban> Bans { get; set; }
        public DbSet<BannedHash> BannedHashes { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<WordFilter> WordFilters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}