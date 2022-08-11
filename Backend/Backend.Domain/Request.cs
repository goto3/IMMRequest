using System;
using System.Collections.Generic;

namespace Backend.Domain
{
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public Applicant Applicant { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public TopicType TopicType { get; set; }
        public List<AdditionalFieldData> AdditionalFields { get; set; }
    }
}
