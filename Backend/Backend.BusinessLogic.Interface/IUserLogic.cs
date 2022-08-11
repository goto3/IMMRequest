using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.BusinessLogic.Interface
{
    public interface IUserLogic
    {
        User Create(User entity);
        void Remove(Guid id);
        User Update(User entity);
        User Get(Guid id);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
    }
}
