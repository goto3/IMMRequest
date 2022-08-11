using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class TopicDT
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TopicTypeDT> TopicTypes { get; set; }

        public TopicDT(Topic entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            TopicTypes = new List<TopicTypeDT>();
            if (entity.TopicTypes != null)
            {
                entity.TopicTypes.ForEach(tt => TopicTypes.Add(new TopicTypeDT(tt)));
            }
        }

    }
}