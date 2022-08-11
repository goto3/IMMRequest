using System;
using System.Collections.Generic;
using IMMRequestDataImport.domain;

namespace Backend.WebApi.Models
{
    public class ImportFieldDT
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
        public List<string> Args { get; set; }


        public ImportFieldDT(ImportField entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Data = entity.Data;
            this.Type = entity.Type;
            this.Args = entity.Args;
        }
    }
}
