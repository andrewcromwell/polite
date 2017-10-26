namespace polite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BThreads", "comment", c => c.String(maxLength: 500));
            AlterColumn("dbo.BThreads", "subject", c => c.String(maxLength: 50));
            AlterColumn("dbo.BThreads", "email", c => c.String(maxLength: 50));
            AlterColumn("dbo.BThreads", "name", c => c.String(maxLength: 50));
            AlterColumn("dbo.TComments", "comment", c => c.String(maxLength: 500));
            AlterColumn("dbo.TComments", "subject", c => c.String(maxLength: 50));
            AlterColumn("dbo.TComments", "email", c => c.String(maxLength: 50));
            AlterColumn("dbo.TComments", "name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TComments", "name", c => c.String());
            AlterColumn("dbo.TComments", "email", c => c.String());
            AlterColumn("dbo.TComments", "subject", c => c.String());
            AlterColumn("dbo.TComments", "comment", c => c.String());
            AlterColumn("dbo.BThreads", "name", c => c.String());
            AlterColumn("dbo.BThreads", "email", c => c.String());
            AlterColumn("dbo.BThreads", "subject", c => c.String());
            AlterColumn("dbo.BThreads", "comment", c => c.String());
        }
    }
}
