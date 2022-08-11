using System;
using System.Collections.Generic;
using IMMRequestDataImport.domain;

namespace IMMRequestDataImport
{
    public interface IDataImport
    {
        DllInfo GetDllInfo();
        List<ImportArea> GetNewData(List<ImportField> fields);
    }
}