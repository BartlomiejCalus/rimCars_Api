using Microsoft.AspNetCore.Mvc;
using rimCars_Api.Entities;
using rimCars_Api;
using rimCars_Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace rimCars_Api.Controllers
{
    [Route("api/salon")]
    public class SalonControllers : ControllerBase
    {
        private readonly SalonsDbContext _dbContext;
        private readonly IMapper _mapper;

        public SalonControllers(SalonsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpGet]
        public  ActionResult<IEnumerable<SalonDto>> GetAll()
        {
            var result = _dbContext
                .Salons
                .Include(s => s.Address)
                .Include(s => s.Rims)
                .ToList();

            var salonsDto = _mapper.Map<List<SalonDto>>(result);
            return Ok(salonsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<SalonDto> Get([FromRoute]int id)
        {
            var result = _dbContext
                .Salons
                .Include(s => s.Address)
                .Include(s => s.Rims)
                .FirstOrDefault(r => r.Id == id);

            if(result == null)
            {
                return NotFound();
            }
            var salonDto = _mapper.Map<SalonDto>(result);
            return Ok(salonDto);
        }

    }
}
