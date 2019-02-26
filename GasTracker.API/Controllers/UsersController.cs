using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasTracker.API.Data.Models;
using GasTracker.API.Services;
using GasTracker.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _user;

        public UsersController(IUserService user){
            _user = user;
        }

        // GET api/users/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_user.GetUsers());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return Ok(_user.GetUser(id));
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _user.AddUser(user);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            if (user.UserId == 0) user.UserId = id; 
            _user.UpdateUser(user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] User user)
        {
            _user.DeleteUser(user);
        }
    }
}
