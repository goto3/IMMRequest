using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class RequestRepository : BaseRepository<Request>
    {
        public RequestRepository(DbContext context)
        {
            Context = context;
        }

        public override Request Get(Guid id)
        {
            return Context.Set<Request>().Include(r => r.Applicant)
                .Include(r => r.TopicType)
                .Include(r => r.AdditionalFields).ThenInclude(afd => afd.AdditionalField)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Request> GetAll()
        {
            return Context.Set<Request>().Include(r => r.Applicant)
                .Include(r => r.TopicType)
                .Include(r => r.AdditionalFields).ThenInclude(afd => afd.AdditionalField)
                .ToList();
        }
    }
}