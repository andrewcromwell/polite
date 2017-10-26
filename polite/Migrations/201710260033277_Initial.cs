namespace polite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        shortName = c.String(),
                        longName = c.String(),
                        description = c.String(),
                        maxPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BThreads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoardID = c.Int(nullable: false),
                        comment = c.String(),
                        subject = c.String(),
                        email = c.String(),
                        name = c.String(),
                        timestamp = c.DateTime(nullable: false),
                        threadID = c.Int(nullable: false),
                        ipAddress = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Boards", t => t.BoardID)
                .Index(t => t.BoardID);
            
            CreateTable(
                "dbo.TComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comment = c.String(),
                        subject = c.String(),
                        email = c.String(),
                        name = c.String(),
                        timestamp = c.DateTime(nullable: false),
                        commentId = c.Int(nullable: false),
                        ipAddress = c.String(),
                        BThreadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BThreads", t => t.BThreadId)
                .Index(t => t.BThreadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TComments", "BThreadId", "dbo.BThreads");
            DropForeignKey("dbo.BThreads", "BoardID", "dbo.Boards");
            DropIndex("dbo.TComments", new[] { "BThreadId" });
            DropIndex("dbo.BThreads", new[] { "BoardID" });
            DropTable("dbo.TComments");
            DropTable("dbo.BThreads");
            DropTable("dbo.Boards");
        }
    }
}
