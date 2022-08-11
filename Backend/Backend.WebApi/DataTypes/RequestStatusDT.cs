using System;
using System.Collections.Generic;
using Backend.Domain;
using Backend.Tools;

namespace Backend.WebApi.Models
{
    public class RequestStatusDT
    {
        public string Status { get; set; }
        public string StatusDescription { get; set; }

        public RequestStatusDT() { }

        public Request ToEntity() => new Request()
        {
            Status = this.Status,
            StatusDescription = this.StatusDescription
        };


    }
}