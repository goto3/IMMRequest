using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class TopicRepository : BaseRepository<Topic>
    {
        public TopicRepository(DbContext context)
        {
            Context = context;
        }

        public override Topic Get(Guid id)
        {
            return Context.Set<Topic>().Include(t => t.TopicTypes)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Topic> GetAll()
        {
            return Context.Set<Topic>().Include(t => t.TopicTypes).ToList();
        }
    }
}