using System;
using System.Collections.Generic;

namespace IMMRequestDataImport.domain
{
    public class ImportField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
        public List<string> Args { get; set; }
    }
}
