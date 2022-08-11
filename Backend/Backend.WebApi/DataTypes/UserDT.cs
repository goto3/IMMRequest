using System;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class UserDT
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserDT() { }
        public UserDT(User entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Email = entity.Email;
            this.Password = entity.Password;
        }
        public User ToEntity()
        {
            User user = new User()
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                Password = this.Password,
            };
            return user;
        }

    }
}