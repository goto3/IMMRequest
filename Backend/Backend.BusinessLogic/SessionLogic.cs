using Backend.BusinessLogic.Interface;
using Backend.Domain;
using Backend.Repository.Interface;
using Backend.Tools;
using System;
using System.Linq;

namespace Backend.BusinessLogic
{
    public class SessionLogic : IUserSession
    {
        private IRepository<UserSession> repository;
        private IUserLogic userLogic;
        public SessionLogic(IRepository<UserSession> repository, IUserLogic userLogic)
        {
            this.repository = repository;
            this.userLogic = userLogic;
        }

        public Guid Login(string email, string password)
        {
            ValidateCredentials(email, password);
            var user = Get(email, password);
            Guid token = Guid.NewGuid();
            UserSession us = new UserSession() { Token = token, User = user, Date = DateTime.Now };
            repository.Add(us);
            repository.Save();
            return token;
        }

        public void Logout(Guid token)
        {
            var session = repository.Get(token);
            if (session == null)
            {
                throw new BackendException("ERR_SESSION_TOKEN_NULL_NOT_FOUND");
            }
            repository.Remove(session);
            repository.Save();
        }

        public bool IsLogued(Guid token)
        {
            int exist = repository.GetAll().Where(x => x.Token == token).ToList().Count;
            return exist > 0 ? true : false;
        }

        private User Get(string email, string password)
        {
            var user = userLogic.GetAll().Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).FirstOrDefault();
            if (user == null)
            {
                throw new BackendException("ERR_SESSION_CREDENTIALS");
            }
            return user;
        }

        private void ValidateCredentials(string email, string pass)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new BackendException("ERR_SESSION_EMAIL");
            }
            if (string.IsNullOrEmpty(pass))
            {
                throw new BackendException("ERR_SESSION_PASSWORD");
            }
        }

    }
}
