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
    public class TopicTypesController : ControllerBase
    {
        private ITopicTypeLogic topicTypeLogic;
        public TopicTypesController(ITopicTypeLogic requestLogic) : base()
        {
            this.topicTypeLogic = requestLogic;
        }

        /// <summary>
        /// Creates a new TopicType.
        /// </summary>
        [HttpPost]
        [AuthFilter]
        public IActionResult Post([FromHeader] string Auth, [FromBody] TopicTypeDT topicType)
        {
            var newTopicType = topicTypeLogic.Create(topicType.ToEntity());
            var newTTModel = new TopicTypeDT(newTopicType);
            return Created("TopicType created successfully.", newTTModel);
        }

        /// <summary>
        /// Shows a specific TopicType.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var topicType = topicTypeLogic.Get(id);
            if (topicType == null)
            {
                throw new BackendException("ERR_TOPICTYPE_NOT_FOUND", id.ToString());
            }
            return Ok(new TopicTypeDT(topicType));
        }

        /// <summary>
        /// Shows all TopicTypes.
        /// </summary>
        [HttpGet]
        [AuthFilter]
        public IActionResult GetAll([FromHeader] string Auth)
        {
            var topicTypes = topicTypeLogic.GetAll();
            var ttModelList = new List<TopicTypeDT>();
            topicTypes.ToList().ForEach(tt => ttModelList.Add(new TopicTypeDT(tt)));
            return Ok(ttModelList);
        }

        /// <summary>
        /// Generates Report B
        /// </summary>
        [HttpGet("ReporteB")]
        [AuthFilter]
        public IActionResult GetReportB([FromHeader] string Auth, string dateStart, string dateEnd)
        {
            var dt1 = new DateTime();
            var dt2 = new DateTime();
            try
            {
                dt1 = DateTime.Parse(dateStart);
                dt2 = DateTime.Parse(dateEnd).AddDays(1);
            }
            catch
            {
                throw new BackendException("ERR_TOPICTYPE_DATE_FORMAT");
            }
            var ttList = topicTypeLogic.GetReportB(dt1, dt2);
            var ttModelList = new List<TopicTypeReportBDT>();
            ttList.ToList().ForEach(tt => ttModelList.Add(new TopicTypeReportBDT(tt)));
            return Ok(ttModelList);
        }


        /// <summary>
        /// Deletes a specific TopicType.
        /// </summary>
        [HttpDelete("{id}")]
        [AuthFilter]
        public IActionResult Delete([FromHeader] string Auth, Guid id)
        {
            topicTypeLogic.Remove(id);
            return Ok("TopicType removed successfully");
        }

    }
}