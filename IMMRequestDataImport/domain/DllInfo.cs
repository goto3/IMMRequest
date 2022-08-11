using System;
using System.Collections.Generic;

namespace IMMRequestDataImport.domain
{
    public class DllInfo
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public List<ImportField> fields { get; set; }
    }
}
