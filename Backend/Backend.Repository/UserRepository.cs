using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext context)
        {
            Context = context;
        }

        public override User Get(Guid id)
        {
            return Context.Set<User>()
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<User> GetAll()
        {
            return Context.Set<User>().ToList();
        }
    }
}