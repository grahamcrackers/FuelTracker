using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasTracker.API.Data.Models;
using GasTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GasTracker.API.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly IService<Trip> _trip;

        public TripController(IService<Trip> trip){
            _trip = trip;
        }

        // GET api/trips
        [HttpGet]
        public ActionResult<IEnumerable<Trip>> Get()
        {
            return Ok(_trip.Get());
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(int id)
        {
            return Ok(_trip.Get(id));
        }

        // POST api/trips
        [HttpPost]
        public void Post([FromBody] Trip entity)
        {
            _trip.Add(entity);
        }

        // PUT api/trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trip entity)
        {
            if (entity.TripId == 0) entity.TripId = id; 
            _trip.Update(entity);
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _trip.Delete(id);
        }
    }
}
