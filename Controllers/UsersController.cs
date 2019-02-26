using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasTracker.Data.DTO;
using GasTracker.Data.Models;
using GasTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IService<User> _user;
        private readonly IMapper _mapper;

        public UsersController(IService<User> user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        // GET api/users/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _user.Get();
            // var dto = _mapper.Map<UserDTO>(users);
            return Ok(users);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return Ok(_user.Get(id));
        }

        // POST api/users
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _user.Add(user);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            if (user.UserId == 0) user.UserId = id; 
            _user.Update(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _user.Delete(id);
        }
    }
}
