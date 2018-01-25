using FishFourm.Core.Entity;
using FishFourm.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace FishFourm.Test.InitialData
{
    public class FishFourmInitialDataBuilder
    {
        public void Build(FishFourmDbContext context)
        {
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
                users[3] );

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
