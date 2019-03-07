using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasTracker.API.Data.DTO;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GasTracker.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _log;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        

        public UsersController(ILogger<UsersController> log, IUnitOfWork uow, IMapper mapper)
        {
            _log = log;
            _uow = uow;
            _mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var users = _uow.GetRepository<User>().Get();
            _log.LogInformation("Get Users", users.ToString());
            var dtoList = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return Ok(dtoList);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var user = _uow.GetRepository<User>().Get(x => x.UserId == id).FirstOrDefault();
            _log.LogInformation("Get Users", user.ToString());
            var dto = _mapper.Map<User, UserDTO>(user);

            return Ok(dto);
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] UserDTO user)
        {
            var fromDto = _mapper.Map<UserDTO, User>(user);
            _log.LogInformation("Add User", fromDto.ToString());
            var added = _uow.GetRepository<User>().Add(fromDto);
            _uow.SaveChanges();

            var dto = _mapper.Map<User, UserDTO>(added);

            return Ok(dto);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] UserDTO user)
        {
            // Make sure it exists
            var thing = _uow.GetRepository<User>().Get(x => x.UserId == id).FirstOrDefault();
            
            var fromDto = _mapper.Map<UserDTO, User>(user);
            
            thing = fromDto;
            // _log.LogInformation("Update User", fromDto.ToString());
            var updated = _uow.GetRepository<User>().Update(thing);
            _uow.SaveChanges();

            var dto = _mapper.Map<User, UserDTO>(updated);

            return Ok(dto);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            var existing = _uow.GetRepository<User>().Get(x => x.UserId == id);
            if (existing != null) _uow.GetRepository<User>().Delete(existing);
            _uow.SaveChanges();

            return Ok();
        }
    }
}
