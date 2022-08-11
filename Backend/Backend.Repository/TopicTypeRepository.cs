using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class TopicTypeRepository : BaseRepository<TopicType>
    {
        public TopicTypeRepository(DbContext context)
        {
            Context = context;
        }

        public override TopicType Get(Guid id)
        {
            return Context.Set<TopicType>().Include(tt => tt.AdditionalFields)
                .Include(tt => tt.Topic)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<TopicType> GetAll()
        {
            return Context.Set<TopicType>().Include(tt => tt.AdditionalFields).Include(tt => tt.Topic).Include(tt => tt.Requests).ToList();
        }
    }
}