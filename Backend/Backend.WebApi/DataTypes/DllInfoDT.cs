using System;
using System.Collections.Generic;
using Backend.Domain;
using IMMRequestDataImport.domain;

namespace Backend.WebApi.Models
{
    public class DllInfoDT
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public List<ImportFieldDT> Fields { get; set; }

        public DllInfoDT(DllInfo entity)
        {
            this.Name = entity.Name;
            this.Info = entity.Info;
            Fields = new List<ImportFieldDT>();
            if (entity.fields != null)
            {
                entity.fields.ForEach(field => Fields.Add(new ImportFieldDT(field)));
            }
        }

    }
}