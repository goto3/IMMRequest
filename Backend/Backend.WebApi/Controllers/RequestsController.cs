using Backend.BusinessLogic.Interface;
using Backend.Tools;
using Backend.WebApi.Filters;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class RequestsController : ControllerBase
    {
        private IRequestLogic requestLogic;
        public RequestsController(IRequestLogic requestLogic) : base()
        {
            this.requestLogic = requestLogic;
        }

        /// <summary>
        /// Creates a new Request.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] RequestDT request)
        {
            var newRequest = requestLogic.Create(request.ToEntity());
            var newRequestModel = new RequestDT(newRequest);
            return Created("Request created successfully.", newRequestModel);
        }

        /// <summary>
        /// Shows a specific Request.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var request = requestLogic.Get(id);
            if (request == null)
            {
                throw new BackendException("ERR_REQUEST_NOT_FOUND", id.ToString());
            }
            return Ok(new RequestDT(request));
        }

        /// <summary>
        /// Shows all Requests.
        /// </summary>
        [HttpGet]
        [AuthFilter]
        public IActionResult GetAll([FromHeader] string Auth)
        {
            var requestsList = requestLogic.GetAll();
            var requestModelList = new List<RequestDT>();
            requestsList.ToList().ForEach(r => requestModelList.Add(new RequestDT(r)));
            return Ok(requestModelList);
        }

        /// <summary>
        /// Shows Applicant requests for a specific date range.
        /// </summary>
        [HttpGet("ReporteA")]
        [AuthFilter]
        public IActionResult GetReportA([FromHeader] string Auth, string email, string dateStart, string dateEnd)
        {
            var dt1 = new DateTime();
            var dt2 = new DateTime();
            try
            {
                dt1 = DateTime.Parse(dateStart);
                dt2 = DateTime.Parse(dateEnd);
            }
            catch
            {
                throw new BackendException("ERR_REQUEST_DATE_FORMAT");
            }
            var requestsList = requestLogic.GetReportA(email, dt1, dt2);
            var requestModelList = new List<RequestDT>();
            requestsList.ToList().ForEach(r => requestModelList.Add(new RequestDT(r)));
            return Ok(requestModelList);
        }

        /// <summary>
        /// Edits Request status and description.
        /// </summary>
        [HttpPut("{id}")]
        [AuthFilter]
        public IActionResult Put([FromHeader] string Auth, Guid id, [FromBody] RequestStatusDT entity)
        {
            var request = entity.ToEntity();
            request.Id = id;
            requestLogic.UpdateStatus(request);
            return Ok("Request status updated successfully");
        }

    }
}