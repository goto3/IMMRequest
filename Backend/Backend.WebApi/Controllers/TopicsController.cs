using System;
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
    public class TopicsController : ControllerBase
    {
        private ILogic<Topic> topicLogic;
        public TopicsController(ILogic<Topic> topicLogic) : base()
        {
            this.topicLogic = topicLogic;
        }

        /// <summary>
        /// Shows a specific Topic.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var topic = topicLogic.Get(id);
            if (topic == null)
            {
                throw new BackendException("ERR_TOPIC_NOT_FOUND", id.ToString());
            }
            return Ok(new TopicDT(topic));
        }

    }
}