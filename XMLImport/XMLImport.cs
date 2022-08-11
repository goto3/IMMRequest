using System;
using System.Collections.Generic;
using IMMRequestDataImport;
using IMMRequestDataImport.domain;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace XMLImport
{
    public class XMLImport : IDataImport
    {
        public DllInfo GetDllInfo()
        {
            return new DllInfo() { Info = "Importar areas, temas y tipos por medio de un archivo .xml ubicado en el servidor. Se debe indicar el nombre de este archivo.", Name = "XMLImport", fields = GetFields() };
        }

        public List<ImportArea> GetNewData(List<ImportField> fields)
        {
            ImportAreas importAreas;
            var xmlFile = fields[0].Data;
            using (StreamReader r = new StreamReader("./ImportedFiles/" + xmlFile))
            {
                string xml = r.ReadToEnd();
                xml = Regex.Replace(xml, @"\t|\n|\r", "");
                Serializer ser = new Serializer();
                importAreas = (ImportAreas)ser.Deserialize<ImportAreas>(xml);
            }
            return importAreas.Areas;
        }

        private List<ImportField> GetFields()
        {
            var infoField = new ImportField() { Id = 0, Name = "XML file name", Type = "text", };
            return new List<ImportField>() { infoField };
        }
    }
}
