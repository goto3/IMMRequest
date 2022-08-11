using System;
using System.Collections.Generic;
using Backend.Domain;
using IMMRequestDataImport.domain;

namespace Backend.BusinessLogic.Interface
{
    public interface IDataImportLogic
    {
        List<DllInfo> GetAll();
        List<Area> Import(string dllName, List<ImportField> fields);
    }
}
