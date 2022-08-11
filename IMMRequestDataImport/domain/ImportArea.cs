using System;
using System.Collections.Generic;

namespace IMMRequestDataImport.domain
{
    public class ImportArea
    {
        public string Name { get; set; }
        public List<ImportTopic> Topics { get; set; }
    }
}