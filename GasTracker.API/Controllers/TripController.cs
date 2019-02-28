using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GasTracker.API.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TripController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET api/trips
        [HttpGet]
        public ActionResult<IEnumerable<Trip>> Get()
        {
            var trips = _uow.GetRepository<Trip>().Get().FirstOrDefault();
            return Ok(trips);
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(int id)
        {
            var trip = _uow.GetRepository<Trip>().Get(x => x.TripId == id).FirstOrDefault();
            return Ok(trip);
        }

        // POST api/trips
        [HttpPost]
        public void Post([FromBody] Trip entity)
        {
            _uow.GetRepository<Trip>().Add(entity);
            _uow.SaveChanges();
        }

        // PUT api/trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trip entity)
        {
            _uow.GetRepository<Trip>().Update(entity);
            _uow.SaveChanges();
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = _uow.GetRepository<Trip>().Get(x => x.TripId == id);
            if (existing != null) _uow.GetRepository<Trip>().Delete(existing);
        }
    }
}
