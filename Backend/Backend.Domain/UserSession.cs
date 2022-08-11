using System;

namespace Backend.Domain
{
    public class UserSession
    {
        public Guid Token { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public UserSession() { }
    }
}
