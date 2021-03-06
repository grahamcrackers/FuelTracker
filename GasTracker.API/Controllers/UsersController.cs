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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        

        public UsersController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET api/users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var users = _uow.GetRepository<User>().Get();
            var dtoList = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

            return Ok(dtoList);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var user = _uow.GetRepository<User>().Get(x => x.UserId == id).FirstOrDefault();
            var dto = _mapper.Map<User, UserDTO>(user);

            return Ok(dto);
        }

        // GET api/users/5
        [HttpGet("{id}/vehicles")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult GetUserVehicles(int id)
        {
            var user = _uow.GetRepository<User>().Get(x => x.UserId == id).FirstOrDefault();
            var dto = _mapper.Map<User, UserDTO>(user);

            var userVehicles = _uow.GetRepository<Vehicle>().Get(x => x.UserId == id).AsEnumerable();
            dto.Vehicles = _mapper.Map<IEnumerable<Vehicle>, ICollection<VehicleDTO>>(userVehicles);
            return Ok(dto);
        }

        // POST api/users
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] UserDTO user)
        {
            var fromDto = _mapper.Map<UserDTO, User>(user);
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
            var fromDto = _mapper.Map<UserDTO, User>(user);
            var updated = _uow.GetRepository<User>().Update(fromDto);
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

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AutoMapper;
// using GasTracker.API.Data.DTO;
// using GasTracker.API.Data.Models;
// using GasTracker.API.Services;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace GasTracker.API.Controllers
// {
//     [Route("api/users")]
//     [ApiController]
//     public class UsersController : ControllerBase
//     {
//         private readonly ILogger _log;
//         private readonly IMapper _mapper;
//         private readonly IUserService _user;

//         public UsersController(ILogger<UsersController> log, IMapper mapper, IUserService user)
//         {
//             _log = log;
//             _mapper = mapper;
//             _user = user;
//         }

//         // GET api/users
//         [HttpGet]
//         [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
//         public IActionResult Get()
//         {
//             var users = _user.Get();
//             var dtoList = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);

//             return Ok(dtoList);
//         }

//         // GET api/users/5
//         [HttpGet("{id}")]
//         [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
//         public IActionResult Get(int id)
//         {
//             var user = _user.Get(id);
//             var dto = _mapper.Map<User, UserDTO>(user);

//             return Ok(dto);
//         }

//         // POST api/users
//         [HttpPost]
//         [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
//         public IActionResult Post([FromBody] UserDTO user)
//         {
//             var fromDto = _mapper.Map<UserDTO, User>(user);
//             var added = _user.Add(fromDto);
//             var dto = _mapper.Map<User, UserDTO>(added);

//             return Ok(dto);
//         }

//         // PUT api/users/5
//         [HttpPut("{id}")]
//         [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
//         public IActionResult Put(int id, [FromBody] UserDTO user)
//         {
//             // Make sure it exists
//             var fromDto = _mapper.Map<UserDTO, User>(user);
//             _user.Update(fromDto);
            
//             var updated = _user.Get(id);
//             var dto = _mapper.Map<User, UserDTO>(updated);

//             return Ok(dto);
//         }

//         // DELETE api/users/5
//         [HttpDelete("{id}")]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         public IActionResult Delete(int id)
//         {
//             var existing = _user.Get(id);
//             if (existing != null) _user.Delete(existing);

//             return Ok();
//         }
//     }
// }
