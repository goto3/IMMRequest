using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class AdditionalFieldRepository : BaseRepository<AdditionalField>
    {
        public AdditionalFieldRepository(DbContext context)
        {
            Context = context;
        }

        public override AdditionalField Get(Guid id)
        {
            return Context.Set<AdditionalField>()
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<AdditionalField> GetAll()
        {
            return Context.Set<AdditionalField>().ToList();
        }
    }
}