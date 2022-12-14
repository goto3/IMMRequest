using Backend.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Backend.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorDT(context.Exception);

            HttpResponse response = context.HttpContext.Response;
            response.Headers.Add("ErrorCode", error.ErrorCode);
            response.Headers.Add("Details", error.Details);
            response.StatusCode = error.Status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(error);
        }
    }
}