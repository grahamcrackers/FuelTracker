using System.Collections.Generic;
using GasTracker.Data.Models;
using GasTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace GasTracker.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IService<Vehicle> _vehicle;

        public VehiclesController(IService<Vehicle>  vehicle){
            _vehicle = vehicle;
        }
        // GET api/trips/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            return Ok(_vehicle.Get());
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(int id)
        {
            return Ok(_vehicle.Get(id));
        }

        // POST api/trips
        [HttpPost]
        public void Post([FromBody] Vehicle entity)
        {
            _vehicle.Add(entity);
        }

        // PUT api/trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vehicle entity)
        {
            if (entity.VehicleId == 0) entity.VehicleId = id; 
            _vehicle.Update(entity);
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _vehicle.Delete(id);
        }
    }
}
