using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Backend.BusinessLogic.Interface;
using Backend.Domain;
using Backend.WebApi.Models;
using Backend.Tools;
using Microsoft.AspNetCore.Http;

namespace Backend.WebApi.Filters
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private IUserSession userSessions;

        public AuthFilter() { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["Auth"];
            if (token == null)
            {
                GenerateError("ERR_AUTH_MISSING", ref context);
                return;
            }
            userSessions = GetSessions(context);
            Guid tokenGuid;
            try
            {
                tokenGuid = Guid.Parse(token);
            }
            catch
            {
                GenerateError("ERR_AUTH_INVALID", ref context);
                return;
            }
            if (!userSessions.IsLogued(tokenGuid))
            {
                GenerateError("ERR_AUTH_INCORRECT", ref context);
                return;
            }
        }

        private void GenerateError(string errorCode, ref AuthorizationFilterContext context)
        {
            var error = new ErrorDT(new BackendException(errorCode));

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = error.Status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(error);
        }

        private static IUserSession GetSessions(AuthorizationFilterContext context)
        {
            return (IUserSession)context.HttpContext.RequestServices.GetService(typeof(IUserSession));
        }

    }
}