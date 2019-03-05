using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasTracker.API.Data.DTO;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GasTracker.API.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VehiclesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        
        // GET api/vehicles
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var vehicles = _uow.GetRepository<Vehicle>().Get();
            // var dtoList = _mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleDTO>>(vehicles);

            return Ok(vehicles);
        }

        // GET api/vehicles/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var vehicle = _uow.GetRepository<Vehicle>().Get(x => x.VehicleId == id).FirstOrDefault();
            // var dto = _mapper.Map<Vehicle, VehicleDTO>(vehicle);

            return Ok(vehicle);
        }

        // POST api/vehicles
        [HttpPost]
        [ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            // var fromDto = _mapper.Map<VehicleDTO, Vehicle>(vehicle);
            var added = _uow.GetRepository<Vehicle>().Add(vehicle);
            _uow.SaveChanges();
            
            // var dto = _mapper.Map<Vehicle, VehicleDTO>(added);

            return Ok(added);
        }

        // PUT api/vehicles/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            // var fromDto = _mapper.Map<VehicleDTO, Vehicle>(vehicle);
            var updated = _uow.GetRepository<Vehicle>().Update(vehicle);
            _uow.SaveChanges();

            // var dto = _mapper.Map<Vehicle, VehicleDTO>(updated);

            return Ok(updated);
        }

        // DELETE api/vehicles/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _uow.GetRepository<Vehicle>().Get(x => x.VehicleId == id);
            if (existing != null) _uow.GetRepository<Vehicle>().Delete(existing);
            _uow.SaveChanges();

            return Ok();
        }
    }
}
