using System;
using System.Collections.Generic;
using Backend.Domain;

namespace Backend.BusinessLogic.Interface
{
    public interface IUserSession
    {
        Guid Login(string userName, string password);
        void Logout(Guid token);
        bool IsLogued(Guid token);
    }
}
