using System;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class AdditionalFieldDataDT
    {
        public Guid AdditionalField { get; set; }
        public string Name { get; set; }
        public string[] Data { get; set; }

        public AdditionalFieldDataDT() { }

        public AdditionalFieldDataDT(AdditionalFieldData entity)
        {
            this.AdditionalField = entity.AdditionalField.Id;
            this.Name = entity.AdditionalField.Name;
            this.Data = entity.Data;
        }

        public AdditionalFieldData ToEntity() => new AdditionalFieldData()
        {
            AdditionalField = new AdditionalField() { Id = this.AdditionalField, Name = this.Name },
            Data = this.Data,
        };

    }
}