using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class AreaRepository : BaseRepository<Area>
    {
        public AreaRepository(DbContext context)
        {
            Context = context;
        }

        public override Area Get(Guid id)
        {
            return Context.Set<Area>().Include(a => a.Topics)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Area> GetAll()
        {
            return Context.Set<Area>().Include(a => a.Topics).ToList();
        }
    }
}