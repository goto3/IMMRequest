using System;

namespace Backend.Domain
{
    public class AdditionalField
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool MultipleValues { get; set; }
        public string[] PossibleValues { get; set; }
        public string FieldType { get; set; }
    }
}
