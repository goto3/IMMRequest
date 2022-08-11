using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.BusinessLogic.Interface;
using Backend.Domain;
using Backend.WebApi.Models;
using Backend.WebApi.Filters;
using Backend.Tools;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class AreasController : ControllerBase
    {
        private ILogic<Area> areaLogic;
        public AreasController(ILogic<Area> areaLogic) : base()
        {
            this.areaLogic = areaLogic;
        }

        /// <summary>
        /// Shows all Areas.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllAreas()
        {
            var areaList = new List<AreaDT>();
            areaLogic.GetAll().ToList().ForEach(a => areaList.Add(new AreaDT(a)));
            return Ok(areaList);
        }

        /// <summary>
        /// Shows a specific Area.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var area = areaLogic.Get(id);
            if (area == null)
            {
                throw new BackendException("ERR_AREA_NOT_FOUND", id.ToString());
            }
            return Ok(new AreaDT(area));
        }

    }
}