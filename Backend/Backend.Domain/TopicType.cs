using System;
using System.Collections.Generic;

namespace Backend.Domain
{
    public class TopicType
    {
        public Guid Id { get; set; }
        public Topic Topic { get; set; }
        public string Name { get; set; }
        public List<AdditionalField> AdditionalFields { get; set; }
        public List<Request> Requests { get; set; }
        public DateTime Created { get; set; }
    }
}
