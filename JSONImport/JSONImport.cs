using System;
using System.Collections.Generic;
using IMMRequestDataImport;
using IMMRequestDataImport.domain;
using System.Text.Json;
using System.IO;

namespace JSONImport
{
    public class JSONImport : IDataImport
    {
        public DllInfo GetDllInfo()
        {
            return new DllInfo() { Info = "Importar areas, temas y tipos por medio de un archivo .json ubicado en el servidor. Se debe indicar el nombre de este archivo.", Name = "JSONImport", fields = GetFields() };
        }

        public List<ImportArea> GetNewData(List<ImportField> fields)
        {
            List<ImportArea> areas;
            var jsonFile = fields[0].Data;
            using (StreamReader r = new StreamReader("./ImportedFiles/" + jsonFile))
            {
                string json = r.ReadToEnd();
                areas = JsonSerializer.Deserialize<List<ImportArea>>(json);
            }
            return areas;
        }

        private List<ImportField> GetFields()
        {
            var infoField = new ImportField() { Id = 0, Name = "JSON file name", Type = "text", };
            return new List<ImportField>() { infoField };
        }
    }
}
