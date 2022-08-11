using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.BusinessLogic.Interface
{
    public interface IRequestLogic
    {
        Request Create(Request entity);
        void Remove(Guid id);
        void UpdateStatus(Request entity);
        Request Get(Guid id);
        IEnumerable<Request> GetAll();
        IEnumerable<Request> GetReportA(string email, DateTime dt1, DateTime dt2);
    }
}
