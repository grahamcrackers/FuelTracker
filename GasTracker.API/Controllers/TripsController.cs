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
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TripsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET api/trips
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trip>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var trips = _uow.GetRepository<Trip>().Get();
            // var dtoList = _mapper.Map<IEnumerable<Trip>, IEnumerable<TripDTO>>(trips);

            return Ok(trips);
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Trip), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var trip = _uow.GetRepository<Trip>().Get(x => x.TripId == id).FirstOrDefault();
            // var dto = _mapper.Map<Trip, TripDTO>(trip);

            return Ok(trip);
        }

        // POST api/trips
        [HttpPost]
        [ProducesResponseType(typeof(Trip), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Trip trip)
        {
            // var fromDto = _mapper.Map<TripDTO, Trip>(trip);
            var added = _uow.GetRepository<Trip>().Add(trip);
            _uow.SaveChanges();

            // var dto = _mapper.Map<Trip, TripDTO>(added);

            return Ok(added);
        }

        // PUT api/trips/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Trip), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] Trip trip)
        {
            // var fromDto = _mapper.Map<TripDTO, Trip>(trip);
            var updated = _uow.GetRepository<Trip>().Update(trip);
            _uow.SaveChanges();

            // var dto = _mapper.Map<Trip, TripDTO>(updated);

            return Ok(updated);
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = _uow.GetRepository<Trip>().Get(x => x.TripId == id);
            if (existing != null) _uow.GetRepository<Trip>().Delete(existing);
            _uow.SaveChanges();
        }
    }
}
