using System;
using System.Collections.Generic;

namespace IMMRequestDataImport.domain
{
    public class ImportTopic
    {
        public string Name { get; set; }
        public List<ImportTopicType> TopicTypes { get; set; }
    }
}
