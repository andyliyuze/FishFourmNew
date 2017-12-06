using Abp.Domain.Entities;
using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishFourm.Core.Entity
{
    public class User : Entity<Guid>
    {
        public User() { }

        public User(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public User(Guid id)
        {
            Id = id;
        }
       
        public string Name { get; private set; }
    }
}
