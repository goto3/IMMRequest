using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.BusinessLogic.Interface;
using Backend.WebApi.Filters;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Cors;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class SessionsController : ControllerBase
    {
        private IUserSession sessions;
        private IUserLogic users;

        public SessionsController(IUserSession sessions, IUserLogic users) : base()
        {
            this.sessions = sessions;
            this.users = users;
        }

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost]
        public IActionResult Login([FromBody] LoginDT login)
        {
            var token = sessions.Login(login.Email, login.Password);
            var user = users.GetByEmail(login.Email);
            var userTokenDT = new UserTokenDT(user);
            userTokenDT.Token = token.ToString();
            return Created("Successfully logged in. Session token: " + token, userTokenDT);
        }

        /// <summary>
        /// Logout
        /// </summary>
        [HttpDelete]
        public IActionResult Logout([FromHeader] Guid token)
        {
            sessions.Logout(token);
            return Ok("Successfully logged out of session " + token);
        }
    }
}