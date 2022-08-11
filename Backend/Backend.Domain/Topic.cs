using System;
using System.Collections.Generic;

namespace Backend.Domain
{
    public class Topic
    {
        public Guid Id { get; set; }
        public Area Area { get; set; }
        public string Name { get; set; }
        public List<TopicType> TopicTypes { get; set; }
    }
}
