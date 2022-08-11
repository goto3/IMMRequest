using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class AdditionalFieldDataRepository : BaseRepository<AdditionalFieldData>
    {
        public AdditionalFieldDataRepository(DbContext context)
        {
            Context = context;
        }

        public override AdditionalFieldData Get(Guid id)
        {
            return Context.Set<AdditionalFieldData>().Include(afd => afd.AdditionalField)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<AdditionalFieldData> GetAll()
        {
            return Context.Set<AdditionalFieldData>().ToList();
        }
    }
}