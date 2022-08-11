using Backend.BusinessLogic.Interface;
using Backend.Tools;
using Backend.WebApi.Filters;
using Backend.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class UsersController : ControllerBase
    {
        private IUserLogic users;
        public UsersController(IUserLogic users) : base()
        {
            this.users = users;
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>        
        [HttpPost]
        public IActionResult Post([FromBody] UserDT user)
        {
            var newUser = users.Create(user.ToEntity());
            var newUserModel = new UserDT(newUser);
            return Created("User created successfully", newUserModel);
        }

        /// <summary>
        /// Shows a specific User.
        /// </summary>         
        [HttpGet("{id}")]
        [AuthFilter]
        public IActionResult Get([FromHeader] string Auth, Guid id)
        {
            var user = users.Get(id);
            if (user == null)
            {
                throw new BackendException("ERR_USERS_GET_INCORRECT", id.ToString());
            }
            return Ok(new UserDT(user));
        }

        /// <summary>
        /// Shows all Users.
        /// </summary>     
        [HttpGet]
        public ActionResult GetAll([FromHeader] string Auth)
        {
            return Ok(users.GetAll());
        }

        /// <summary>
        /// Edits a specific User.
        /// </summary>  
        [HttpPut("{id}")]
        [AuthFilter]
        public IActionResult Put([FromHeader] string Auth, Guid id, [FromBody] UserDT user)
        {
            user.Id = id;
            var userEntity = user.ToEntity();
            users.Update(userEntity);
            return Ok("User updated successfully");
        }

        /// <summary>
        /// Deletes a specific User.
        /// </summary>  
        [HttpDelete("{id}")]
        [AuthFilter]
        public IActionResult Delete([FromHeader] string Auth, Guid id)
        {
            users.Remove(id);
            return Ok("User removed successfully");
        }
    }
}