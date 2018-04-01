namespace polite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompositeKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "parentId", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "parentId" });
            DropPrimaryKey("dbo.Posts");
            AddColumn("dbo.Posts", "Parent_ID", c => c.Int());
            AddColumn("dbo.Posts", "Parent_BoardID", c => c.Int());
            AlterColumn("dbo.Posts", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "bumped", c => c.DateTime());
            AddPrimaryKey("dbo.Posts", new[] { "ID", "BoardID" });
            CreateIndex("dbo.Posts", new[] { "Parent_ID", "Parent_BoardID" });
            AddForeignKey("dbo.Posts", new[] { "Parent_ID", "Parent_BoardID" }, "dbo.Posts", new[] { "ID", "BoardID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", new[] { "Parent_ID", "Parent_BoardID" }, "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "Parent_ID", "Parent_BoardID" });
            DropPrimaryKey("dbo.Posts");
            AlterColumn("dbo.Posts", "bumped", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Posts", "Parent_BoardID");
            DropColumn("dbo.Posts", "Parent_ID");
            AddPrimaryKey("dbo.Posts", "ID");
            CreateIndex("dbo.Posts", "parentId");
            AddForeignKey("dbo.Posts", "parentId", "dbo.Posts", "ID");
        }
    }
}
