namespace polite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannedHashes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        md5 = c.String(),
                        banTime = c.DateTime(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Bans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        type = c.Int(nullable: false),
                        expired = c.Boolean(nullable: false),
                        allowRead = c.Boolean(nullable: false),
                        IP = c.String(),
                        ipmd5 = c.String(),
                        global = c.Boolean(nullable: false),
                        boards = c.String(),
                        by = c.String(),
                        at = c.DateTime(nullable: false),
                        until = c.DateTime(nullable: false),
                        reason = c.String(),
                        staffnote = c.String(),
                        appeal = c.String(),
                        appealAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        order = c.Int(nullable: false),
                        shortName = c.String(),
                        type = c.Int(nullable: false),
                        start = c.Int(nullable: false),
                        longName = c.String(),
                        description = c.String(),
                        sectionID = c.Int(nullable: false),
                        maxImageSize = c.Int(nullable: false),
                        maxPages = c.Int(nullable: false),
                        maxAge = c.Int(nullable: false),
                        markPage = c.Int(nullable: false),
                        maxReplies = c.Int(nullable: false),
                        messageLength = c.Int(nullable: false),
                        createdOn = c.DateTime(nullable: false),
                        locked = c.Boolean(nullable: false),
                        includeHeader = c.String(),
                        redirectToThread = c.Boolean(nullable: false),
                        anonymous = c.String(),
                        forcedAnon = c.Boolean(nullable: false),
                        trial = c.Boolean(nullable: false),
                        popular = c.Boolean(nullable: false),
                        defaultStyle = c.String(),
                        useIdentities = c.Boolean(nullable: false),
                        maxPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sections", t => t.sectionID)
                .Index(t => t.sectionID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        parentId = c.Int(),
                        BoardID = c.Int(nullable: false),
                        name = c.String(),
                        tripcode = c.String(),
                        email = c.String(),
                        subject = c.String(),
                        message = c.String(),
                        password = c.String(),
                        file = c.String(),
                        filemd5 = c.String(),
                        fileType = c.String(),
                        fileOriginal = c.String(),
                        fileSize = c.Int(),
                        imageW = c.Short(),
                        imageH = c.Short(),
                        thumbW = c.Short(),
                        thumbH = c.Short(),
                        ip = c.String(),
                        ipmd5 = c.String(),
                        timestamp = c.DateTime(nullable: false),
                        stickied = c.Boolean(nullable: false),
                        locked = c.Boolean(nullable: false),
                        deletedTimestamp = c.DateTime(),
                        isDeleted = c.Boolean(nullable: false),
                        bumped = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID, t.BoardID })
                .ForeignKey("dbo.Boards", t => t.BoardID)
                .ForeignKey("dbo.Posts", t => new { t.parentId, t.BoardID })
                .Index(t => new { t.parentId, t.BoardID })
                .Index(t => t.BoardID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        order = c.Int(nullable: false),
                        hidden = c.Boolean(nullable: false),
                        name = c.String(),
                        abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LoginAttempts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        IP = c.String(),
                        timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        salt = c.String(),
                        type = c.Int(nullable: false),
                        boards = c.String(),
                        addedOn = c.DateTime(nullable: false),
                        lastActive = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WordFilters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        word = c.String(),
                        replacedBy = c.String(),
                        boards = c.String(),
                        created = c.DateTime(nullable: false),
                        regex = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boards", "sectionID", "dbo.Sections");
            DropForeignKey("dbo.Posts", new[] { "parentId", "BoardID" }, "dbo.Posts");
            DropForeignKey("dbo.Posts", "BoardID", "dbo.Boards");
            DropIndex("dbo.Posts", new[] { "BoardID" });
            DropIndex("dbo.Posts", new[] { "parentId", "BoardID" });
            DropIndex("dbo.Boards", new[] { "sectionID" });
            DropTable("dbo.WordFilters");
            DropTable("dbo.StaffMembers");
            DropTable("dbo.LoginAttempts");
            DropTable("dbo.Sections");
            DropTable("dbo.Posts");
            DropTable("dbo.Boards");
            DropTable("dbo.Bans");
            DropTable("dbo.BannedHashes");
        }
    }
}
