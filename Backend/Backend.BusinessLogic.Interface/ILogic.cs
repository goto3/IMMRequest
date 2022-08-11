using System;
using System.Collections.Generic;

namespace Backend.BusinessLogic.Interface
{
    public interface ILogic<T>
    {
        T Create(T entity);
        void Remove(Guid id);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Validate(T entity);
    }
}
