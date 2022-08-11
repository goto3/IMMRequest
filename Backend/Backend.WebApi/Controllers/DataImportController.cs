using System;
using Microsoft.AspNetCore.Mvc;
using Backend.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Backend.WebApi.Models;
using Backend.Tools;
using IMMRequestDataImport.domain;
using Backend.BusinessLogic.Interface;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class DataImportController : ControllerBase
    {
        private IDataImportLogic dataImportLogic;

        public DataImportController(IDataImportLogic dataImportLogic) : base()
        {
            this.dataImportLogic = dataImportLogic;
        }

        /// <summary>
        /// Import Data
        /// </summary>
        [HttpPost]
        [AuthFilter]
        public ActionResult ImportData([FromHeader] string Auth, string dllName, List<ImportField> fields)
        {
            var importedAreas = dataImportLogic.Import(dllName, fields);
            var importedAreasModel = new List<AreaDT>();
            importedAreas.ForEach(a => importedAreasModel.Add(new AreaDT(a)));

            return Ok(importedAreasModel);
        }

        /// <summary>
        /// Get all dll infos with fields
        /// </summary>
        [HttpGet]
        [AuthFilter]
        public ActionResult getAll([FromHeader] string Auth)
        {
            var dllInfoList = new List<DllInfoDT>();
            dataImportLogic.GetAll().ForEach(info => dllInfoList.Add(new DllInfoDT(info)));

            return Ok(dllInfoList);
        }
    }
}