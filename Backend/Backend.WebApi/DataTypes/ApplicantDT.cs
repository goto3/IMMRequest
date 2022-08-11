using System;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class ApplicantDT
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ApplicantDT() { }

        public ApplicantDT(Applicant entity)
        {
            this.Name = entity.Name;
            this.Email = entity.Email;
            this.PhoneNumber = entity.PhoneNumber;
        }

        public Applicant ToEntity() => new Applicant()
        {
            Name = this.Name,
            Email = this.Email,
            PhoneNumber = this.PhoneNumber
        };
    }
}