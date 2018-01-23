using Abp.EntityFramework;
using FishFourm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;
using System.Text;

namespace FishFourm.EntityFramework
{
    public class FishFourmDbContext : AbpDbContext
    {
        public virtual IDbSet<Post> Post { get; set; }

        public virtual IDbSet<CommentDTO> Comment { get; set; }

        public virtual IDbSet<User> User { get; set; }

        public FishFourmDbContext()
           : base("FishFourm")
        {

        }
        //public FishFourmDbContext(string nameOrConnectionString)
        //        : base(nameOrConnectionString)
        //{

        //}
        //This constructor is used in tests
        public FishFourmDbContext(DbConnection connection)
                : base(connection, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                   
            modelBuilder.Types().Configure(d =>
            {
                var nonPublicProperties = d.ClrType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var p in nonPublicProperties)
                {
                    d.Property(p).HasColumnName(p.Name);
                }
            });
        }
    }
}
