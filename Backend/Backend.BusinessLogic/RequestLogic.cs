using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using Backend.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Backend.BusinessLogic
{
    public class RequestLogic : IRequestLogic
    {
        private IRepository<Request> requestRepository;
        private ITopicTypeLogic TopicTypeLogic;
        private ILogic<AdditionalFieldData> AdditionalFieldDataLogic;

        public RequestLogic(IRepository<Request> requestRepo, ITopicTypeLogic TopicTypeLogic,
            ILogic<AdditionalFieldData> AdditionalFieldDataLogic)
        {
            this.requestRepository = requestRepo;
            this.TopicTypeLogic = TopicTypeLogic;
            this.AdditionalFieldDataLogic = AdditionalFieldDataLogic;
        }

        public Request Create(Request request)
        {
            GenerateRequestData(request);
            Validate(request);
            CreateAdditionalFieldsData(request);
            requestRepository.Add(request);
            requestRepository.Save();
            return request;
        }

        public Request Get(Guid id)
        {
            var request = requestRepository.Get(id);
            return request;
        }

        public IEnumerable<Request> GetAll()
        {
            return requestRepository.GetAll();
        }

        public IEnumerable<Request> GetReportA(string email, DateTime dt1, DateTime dt2)
        {
            if (dt1 > dt2)
            {
                throw new BackendException("ERR_REQUEST_DATES_INVALID");
            }
            var applicantRequests = GetAll().Where(r => r.Applicant.Email.ToLower() == email.ToLower()
                && (dt1 <= r.Date) && (dt2 >= r.Date)).ToList();
            return applicantRequests;
        }

        public void UpdateStatus(Request entity)
        {
            var request = ValidateBeforeUpdateStatus(entity);
            request.Status = entity.Status;
            if (!String.IsNullOrEmpty(entity.StatusDescription))
            {
                request.StatusDescription = entity.StatusDescription;
            }
            requestRepository.Update(request);
            requestRepository.Save();
        }

        public void Remove(Guid id)
        {
            Request req = requestRepository.Get(id);
            requestRepository.Remove(req);
            requestRepository.Save();
        }

        private void GenerateRequestData(Request entity)
        {
            ValidateTopicType(entity.TopicType);
            entity.Id = Guid.NewGuid();
            entity.Status = "Created";
            entity.StatusDescription = "";
            entity.Date = DateTime.Now;
            entity.TopicType = TopicTypeLogic.Get(entity.TopicType.Id);
        }

        private void Validate(Request request)
        {
            if (String.IsNullOrEmpty(request.Details))
            {
                throw new BackendException("ERR_REQUEST_DETAILS_NULL_EMPTY");
            }
            if (request.Details.Length > 2000)
            {
                throw new BackendException("ERR_REQUEST_DETAILS_LONG");
            }
            ApplicantLogic.Validate(request.Applicant);
            ValidateTopicType(request.TopicType);
            ValidateAdditionalFields(request.AdditionalFields, request.TopicType);
        }

        private void CreateAdditionalFieldsData(Request request)
        {
            if (request.AdditionalFields != null)
            {
                var oldFields = request.AdditionalFields;
                request.AdditionalFields = new List<AdditionalFieldData>();
                oldFields.ForEach(af => request.AdditionalFields.Add(AdditionalFieldDataLogic.Create(af)));
            }
        }

        private void ValidateTopicType(TopicType topicType)
        {
            if (topicType == null)
            {
                throw new BackendException("ERR_REQUEST_TOPICTYPE_NULL_NOTFOUND");
            }
        }

        private void ValidateAdditionalFields(List<AdditionalFieldData> fields, TopicType topicType)
        {
            if (topicType.AdditionalFields != null)
            {
                foreach (AdditionalField af in topicType.AdditionalFields)
                {
                    if (fields == null || fields.Count == 0 || !fields.Any(fd => fd.AdditionalField.Id == af.Id))
                    {
                        throw new BackendException("ERR_REQUEST_MISSING_ADDITIONALFIELD", af.Id.ToString());
                    }
                }
                if (topicType.AdditionalFields.Count != fields.Count)
                {
                    throw new BackendException("ERR_REQUEST_TOO_MANY_ADDITIONALFIELDS");
                }
                fields.ForEach(f => AdditionalFieldDataLogic.Validate(f));
            }
        }

        private Request ValidateBeforeUpdateStatus(Request entity)
        {
            if (entity == null)
            {
                throw new BackendException("ERR_REQUEST_NULL");
            }
            var request = requestRepository.Get(entity.Id);
            if (request == null)
            {
                throw new BackendException("ERR_REQUEST_NOT_FOUND");
            }
            GetPossibleStatus(entity.Status);
            var possibleStatus = GetPossibleStatus(request.Status);
            if (!possibleStatus.Any(s => s == entity.Status))
            {
                throw new BackendException("ERR_REQUEST_STATUS_UPDATE_PRECEDENCE");
            }
            return request;
        }

        private List<string> GetPossibleStatus(string status)
        {
            var statusList = new List<string>();
            if (status == "Created")
            {
                statusList.Add("Created");
                statusList.Add("In review");
            }
            else if (status == "In review")
            {
                statusList.Add("Created");
                statusList.Add("In review");
                statusList.Add("Accepted");
                statusList.Add("Denied");
            }
            else if (status == "Accepted" || status == "Denied")
            {
                statusList.Add("In review");
                statusList.Add("Accepted");
                statusList.Add("Denied");
                statusList.Add("Closed");
            }
            else if (status == "Closed")
            {
                statusList.Add("Accepted");
                statusList.Add("Denied");
                statusList.Add("Closed");
            }
            else
            {
                throw new BackendException("ERR_REQUEST_STATUS_PARSE");
            }
            return statusList;
        }


    }
}