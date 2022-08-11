using System;
using Backend.Domain;
using Backend.BusinessLogic.Interface;
using Backend.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using Backend.Tools;

namespace Backend.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IRepository<User> repository;

        public UserLogic(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public User Create(User newUserData)
        {
            Validate(newUserData);
            DuplicateCheck(newUserData, true);
            newUserData.Id = Guid.NewGuid();
            repository.Add(newUserData);
            repository.Save();
            return newUserData;
        }

        public User Get(Guid id)
        {
            return repository.Get(id);
        }

        public User GetByEmail(string email)
        {
            return GetAll().ToList().Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return repository.GetAll();
        }

        public void Remove(Guid id)
        {
            User user = repository.Get(id);
            if (user == null)
            {
                throw new BackendException("ERR_USER_NOT_FOUND", id.ToString());
            }
            repository.Remove(user);
            repository.Save();
        }

        public User Update(User entity)
        {
            var user = repository.Get(entity.Id);
            if (user == null)
            {
                throw new BackendException("ERR_USER_NOT_FOUND", entity.Id.ToString());
            }
            if (String.IsNullOrEmpty(entity.Password))
            {
                entity.Password = user.Password;
            }
            Validate(entity);
            DuplicateCheck(entity, false);
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.Password = entity.Password;
            repository.Update(user);
            repository.Save();
            return user;
        }

        private void Validate(User user)
        {
            if (user == null)
            {
                throw new BackendException("ERR_USER_NULL");
            }
            if (String.IsNullOrEmpty(user.Email))
            {
                throw new BackendException("ERR_USER_EMAIL_NULL_EMPTY");
            }
            if (!EmailValidation.IsValid(user.Email))
            {
                throw new BackendException("ERR_USER_EMAIL_INVALID");
            }
            if (String.IsNullOrEmpty(user.Password))
            {
                throw new BackendException("ERR_USER_PASSWORD_NULL_EMPTY");
            }
        }

        private void DuplicateCheck(User user, bool creating)
        {
            bool existUser;
            if (creating)
            {
                existUser = repository.GetAll().Any(u => u.Email.ToLower() == user.Email.ToLower());
            }
            else
            {
                existUser = repository.GetAll().Any(u => u.Email.ToLower() == user.Email.ToLower() && u.Id != user.Id);
            }
            if (existUser)
            {
                throw new BackendException("ERR_USER_DUPLICATE");
            }
        }
    }
}
