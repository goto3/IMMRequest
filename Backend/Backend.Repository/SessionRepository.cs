using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class SessionRepository : BaseRepository<UserSession>
    {
        public SessionRepository(DbContext context)
        {
            Context = context;
        }

        public override UserSession Get(Guid token)
        {
            return Context.Set<UserSession>()
                .FirstOrDefault(x => x.Token == token);
        }

        public override IEnumerable<UserSession> GetAll()
        {
            return Context.Set<UserSession>().ToList();
        }
    }
}