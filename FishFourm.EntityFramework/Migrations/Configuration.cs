namespace FishFourm.EntityFramework.Migrations
{
    using FishFourm.Core.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FishFourm.EntityFramework.FishFourmDbContext>
    {
        public Configuration()
        {

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FishFourm.EntityFramework.FishFourmDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            List<User> users = new List<User>()
            {
                new User("Isaac Asimov1"),
                new User("Isaac Asimov2"),
                new User("Isaac Asimov3"),
                new User("Isaac Asimov4")
            };
            context.User.AddOrUpdate(
                p => p.Id,
                users[0],
                users[1],
                users[2],
                users[3]);
            context.SaveChanges();
            context.Post.AddOrUpdate(
             t => t.Id,
            new Post(users[0].Id, "title1", "body1"),
            new Post(users[1].Id, "title2", "body2"),
            new Post(users[2].Id, "title3", "body3"),
            new Post(users[3].Id, "title4", "body4"));
            context.SaveChanges();
        }
    }
}
