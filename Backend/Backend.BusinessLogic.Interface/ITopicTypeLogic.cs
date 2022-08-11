using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.BusinessLogic.Interface
{
    public interface ITopicTypeLogic
    {
        TopicType Create(TopicType entity);
        void Remove(Guid id);
        TopicType Get(Guid id);
        IEnumerable<TopicType> GetAll();
        void Validate(TopicType entity);
        List<TopicType> GetReportB(DateTime dt1, DateTime dt2);
    }
}
