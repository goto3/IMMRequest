using System;
using System.Collections.Generic;

namespace Backend.Domain
{
    public class AdditionalFieldData
    {
        public Guid Id { get; set; }
        public AdditionalField AdditionalField { get; set; }
        public string[] Data { get; set; }

    }
}
