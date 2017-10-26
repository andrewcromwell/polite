namespace polite.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<polite.Models.ImageBoardDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(polite.Models.ImageBoardDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Boards.AddOrUpdate(b => b.shortName,
            new Board
            {
                shortName = "a",
                longName = "anime & manga",
                description = "discuss anime and manga",
                maxPostId = 0
            },
            new Board
            {
                shortName = "b",
                longName = "random",
                description = "discuss anything",
                maxPostId = 0
            },
            new Board
            {
                shortName = "c",
                longName = "anime/cute",
                description = "discuss cute things",
                maxPostId = 0
            },
            new Board
            {
                shortName = "co",
                longName = "comics & cartoons",
                description = "discuss comics and cartoons",
                maxPostId = 0
            });
        }
    }
}
