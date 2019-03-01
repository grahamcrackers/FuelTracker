using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GasTracker.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _uow.GetRepository<User>().Get();
            // var dto = _mapper.Map<UserDTO>(users);
            return Ok(users);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _uow.GetRepository<User>().Get(x => x.UserId == id).FirstOrDefault();
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            var added = _uow.GetRepository<User>().Add(user);
            _uow.SaveChanges();

            return Ok(added);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            var updated = _uow.GetRepository<User>().Update(user);
            _uow.SaveChanges();

            return Ok(updated);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = _uow.GetRepository<User>().Get(x => x.UserId == id);
            if (existing != null) _uow.GetRepository<User>().Delete(existing);
        }
    }
}
