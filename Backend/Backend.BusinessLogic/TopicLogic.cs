using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;

namespace Backend.BusinessLogic
{
    public class TopicLogic : ILogic<Topic>
    {
        private IRepository<Topic> topicRepository;

        public TopicLogic(IRepository<Topic> topicRepo)
        {
            this.topicRepository = topicRepo;
        }

        public Topic Create(Topic newTopic)
        {
            Validate(newTopic);
            if (Exists(newTopic))
            {
                newTopic = newTopic.Area.Topics.Find(t => t.Name == newTopic.Name);
            }
            else
            {
                topicRepository.Add(newTopic);
                topicRepository.Save();
            }
            return newTopic;
        }

        public Topic Get(Guid id)
        {
            return topicRepository.Get(id);
        }

        public IEnumerable<Topic> GetAll()
        {
            return topicRepository.GetAll();
        }

        public void Remove(Guid id)
        {
            Topic t = topicRepository.Get(id);
            topicRepository.Remove(t);
            topicRepository.Save();
        }

        public void Validate(Topic entity)
        {
        }

        private bool Exists(Topic newTopic)
        {
            return newTopic.Area.Topics != null && newTopic.Area.Topics.Exists(t => t.Name == newTopic.Name);
        }
    }
}
