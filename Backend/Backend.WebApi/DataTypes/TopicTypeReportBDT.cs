using System;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class TopicTypeReportBDT
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int RequestNumber { get; set; }

        public TopicTypeReportBDT(TopicType entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Date = entity.Created.ToString("dd/MM/yyyy hh:mm:ss");
            this.RequestNumber = entity.Requests.Count;
        }
    }
}