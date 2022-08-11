using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using Backend.Tools;

namespace Backend.BusinessLogic
{
    public class TopicTypeLogic : ITopicTypeLogic
    {
        private IRepository<TopicType> topicTypeRepository;
        private ILogic<Topic> topicLogic;
        private ILogic<AdditionalField> additionalFieldLogic;

        public TopicTypeLogic(IRepository<TopicType> topicTypeRepo, ILogic<Topic> topicLogic,
            ILogic<AdditionalField> additionalFieldLogic)
        {
            this.topicTypeRepository = topicTypeRepo;
            this.topicLogic = topicLogic;
            this.additionalFieldLogic = additionalFieldLogic;
        }

        public TopicType Create(TopicType newTopicType)
        {
            Validate(newTopicType);
            GenerateTopicTypeData(newTopicType);
            ValidateTopic(newTopicType.Topic);
            ValidateAdditionalFields(newTopicType.AdditionalFields);
            DuplicateCheck(newTopicType);
            DuplicateCheckAdditionalField(newTopicType.AdditionalFields);
            topicTypeRepository.Add(newTopicType);
            topicTypeRepository.Save();
            return newTopicType;
        }

        public TopicType Get(Guid id)
        {
            return topicTypeRepository.Get(id);
        }

        public IEnumerable<TopicType> GetAll()
        {
            return topicTypeRepository.GetAll();
        }

        public void Remove(Guid id)
        {
            TopicType tt = topicTypeRepository.Get(id);
            if (tt == null)
            {
                throw new BackendException("ERR_TOPICTYPE_NOT_FOUND", id.ToString());
            }
            topicTypeRepository.Remove(tt);
            topicTypeRepository.Save();
        }

        public void Validate(TopicType entity)
        {
            if (String.IsNullOrEmpty(entity.Name))
            {
                throw new BackendException("ERR_TOPICTYPE_NAME_NULL_ERROR");
            }
            ValidateTopic(entity.Topic);
        }

        public List<TopicType> GetReportB(DateTime dt1, DateTime dt2)
        {
            if (dt1 > dt2)
            {
                throw new BackendException("ERR_TOPICTYPE_REPORTB_DATES_INVALID");
            }
            var listTT = GetAll().Where(tt => tt.Requests.Count > 0).ToList();
            listTT.ForEach(tt =>
            {
                tt.Requests = tt.Requests.Where(r => r.Date > dt1 && r.Date < dt2).ToList();
            });
            listTT = listTT.Where(tt => tt.Requests.Count > 0).ToList();
            return listTT.OrderByDescending(tt => tt.Requests.Count).ThenBy(tt => tt.Created).ToList();
        }

        private void GenerateTopicTypeData(TopicType topicType)
        {
            topicType.Id = Guid.NewGuid();
            topicType.Created = DateTime.Now;
            topicType.Topic = topicLogic.Get(topicType.Topic.Id);
            if (topicType.AdditionalFields != null)
            {
                topicType.AdditionalFields.ForEach(af => af.Id = Guid.NewGuid());
            }
        }

        private void ValidateTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new BackendException("ERR_TOPICTYPE_TOPIC_NULL_NOTFOUND");
            }
        }

        private void ValidateAdditionalFields(List<AdditionalField> listAF)
        {
            if (listAF != null)
            {
                listAF.ForEach(af => additionalFieldLogic.Validate(af));
            }
        }

        private void DuplicateCheck(TopicType entity)
        {
            if (entity.Topic.TopicTypes != null)
            {
                bool existsTopicType = entity.Topic.TopicTypes.Any(tt => tt.Name == entity.Name);
                if (existsTopicType)
                {
                    throw new BackendException("ERR_TOPICTYPE_NAME_DUPLICATE");
                }
            }
        }

        private void DuplicateCheckAdditionalField(List<AdditionalField> listAF)
        {
            if (listAF != null)
            {
                bool duplicate = listAF.GroupBy(x => x.Name).Any(g => g.Count() > 1);
                if (duplicate)
                {
                    throw new BackendException("ERR_TOPICTYPE_ADDITONALFIELD_NAME_DUPLICATE");
                }
            }
        }

    }
}
