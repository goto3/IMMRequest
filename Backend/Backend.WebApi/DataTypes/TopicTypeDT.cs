using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class TopicTypeDT
    {
        public Guid Id { get; set; }
        public Guid Topic { get; set; }
        public string Name { get; set; }
        public List<AdditionalFieldDT> AdditionalFields { get; set; }

        public TopicTypeDT()
        {
            AdditionalFields = new List<AdditionalFieldDT>();
        }

        public TopicTypeDT(TopicType entity)
        {
            this.Id = entity.Id;
            if (entity.Topic != null)
            {
                this.Topic = entity.Topic.Id;
            }
            this.Name = entity.Name;
            if (entity.AdditionalFields != null)
            {
                this.AdditionalFields = entity.AdditionalFields.ConvertAll(m => new AdditionalFieldDT(m));
            }
        }

        public TopicType ToEntity() => new TopicType()
        {
            Id = this.Id,
            Topic = new Topic() { Id = this.Topic },
            Name = this.Name,
            AdditionalFields = this.AdditionalFields.ConvertAll(m => m.ToEntity())
        };
    }
}