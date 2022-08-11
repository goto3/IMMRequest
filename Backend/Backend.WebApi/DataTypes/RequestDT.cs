using System;
using System.Collections.Generic;
using Backend.Domain;
using Backend.Tools;

namespace Backend.WebApi.Models
{
    public class RequestDT
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
        public ApplicantDT Applicant { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public Guid TopicType { get; set; }
        public List<AdditionalFieldDataDT> AdditionalFields { get; set; }

        public RequestDT()
        {
            AdditionalFields = new List<AdditionalFieldDataDT>();
        }

        public RequestDT(Request entity)
        {
            this.Id = entity.Id;
            this.Date = entity.Date.ToString("dd/MM/yyyy HH:mm:ss");
            this.Details = entity.Details;
            this.Applicant = new ApplicantDT(entity.Applicant);
            this.Status = entity.Status;
            this.StatusDescription = entity.StatusDescription;
            if (entity.TopicType != null)
                this.TopicType = entity.TopicType.Id;
            this.AdditionalFields = entity.AdditionalFields.ConvertAll(m => new AdditionalFieldDataDT(m));
        }

        public Request ToEntity() => new Request()
        {
            Id = this.Id,
            Details = this.Details,
            Applicant = this.Applicant != null ? this.Applicant.ToEntity() : null,
            Status = this.Status,
            StatusDescription = this.StatusDescription,
            TopicType = new TopicType { Id = this.TopicType },
            AdditionalFields = this.AdditionalFields.ConvertAll(m => m.ToEntity()),
        };

    }
}