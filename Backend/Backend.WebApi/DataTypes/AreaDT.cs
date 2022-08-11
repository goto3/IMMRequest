using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class AreaDT
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TopicDT> Topics { get; set; }

        public AreaDT(Area entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            Topics = new List<TopicDT>();
            if (entity.Topics != null)
            {
                entity.Topics.ForEach(t => Topics.Add(new TopicDT(t)));
            }
        }

    }
}