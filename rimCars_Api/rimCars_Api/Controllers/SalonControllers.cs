using Microsoft.AspNetCore.Mvc;
using rimCars_Api.Entities;
using rimCars_Api;
using rimCars_Api.Models;
using rimCars_Api.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace rimCars_Api.Controllers
{
    [Route("api/salon")]
    public class SalonControllers : ControllerBase
    {
        private readonly ISalonService _salonService;

        public SalonControllers(ISalonService salonService)
        {
            _salonService = salonService;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<SalonDto>> GetAll()
        {
            var result = _salonService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<SalonDto> Get([FromRoute]int id)
        {

            var result = _salonService.GetOne(id);

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddSalon([FromBody]AddSalonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _salonService.Add(dto);
            
            return Created($"api / salon/{id}",null);
        }

        [HttpDelete("{id}")]
        public ActionResult<SalonDto> Delete([FromRoute] int id)
        {
            var isDelete = _salonService.Delete(id);

            if(!isDelete) 
                return NotFound();

            return NoContent();
        }

    }
}
