using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasTracker.API.Data.Models;
using GasTracker.API.Repositories;
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
        
        // GET api/trips/all
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            var entity = _uow.GetRepository<Vehicle>().Get();
            return Ok(entity);
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(int id)
        {
            var entity = _uow.GetRepository<Vehicle>().Get(x => x.VehicleId == id).FirstOrDefault();
            return Ok(entity);
        }

        // POST api/trips
        [HttpPost]
        public void Post([FromBody] Vehicle entity)
        {
            _uow.GetRepository<Vehicle>().Add(entity);
            _uow.SaveChanges();
        }

        // PUT api/trips/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vehicle entity)
        {
            _uow.GetRepository<Vehicle>().Update(entity);
            _uow.SaveChanges();
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existing = _uow.GetRepository<Vehicle>().Get(x => x.VehicleId == id);
            if (existing != null) _uow.GetRepository<Vehicle>().Delete(existing);
        }
    }
}
