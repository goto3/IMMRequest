using System;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class UserTokenDT
    {
        public string id { get; set; }
        public String Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public UserTokenDT() { }
        public UserTokenDT(User entity)
        {
            this.id = entity.Id.ToString();
            this.Name = entity.Name;
            this.Email = entity.Email;
        }

    }
}