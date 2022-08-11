using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class AdditionalFieldDT
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public string multiple { get; set; }
        public List<string> PossibleValues { get; set; }

        public AdditionalFieldDT() { }

        public AdditionalFieldDT(AdditionalField entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.FieldType = entity.FieldType;
            this.multiple = entity.MultipleValues ? "True" : "False";
            if (entity.PossibleValues != null)
            {
                this.PossibleValues = new List<string>(entity.PossibleValues);
            };
        }

        public AdditionalField ToEntity() => new AdditionalField()
        {
            Id = this.Id,
            Name = this.Name,
            FieldType = this.FieldType,
            MultipleValues = (this.multiple == "True"),
            PossibleValues = this.PossibleValues != null ? this.PossibleValues.ToArray() : null
        };

    }
}