using System;
using Backend.Domain;

namespace Backend.WebApi.Models
{
    public class LoginDT
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}