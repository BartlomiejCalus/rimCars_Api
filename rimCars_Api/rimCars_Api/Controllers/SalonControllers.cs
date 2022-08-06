﻿using Microsoft.AspNetCore.Mvc;
using rimCars_Api.Entities;
using rimCars_Api;
using rimCars_Api.Models;
using rimCars_Api.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace rimCars_Api.Controllers
{
    [Route("att")]
    public class SalonControllers : ControllerBase
    {
        private readonly ISalonService _salonService;
        private readonly IRimService _rimService;

        public SalonControllers(ISalonService salonService, IRimService rimService)
        {
            _salonService = salonService;
            _rimService = rimService;
        }

        [HttpGet("salons")]
        public ActionResult<IEnumerable<SalonDto>> GetAll()
        {
            var result = _salonService.GetAll();
            return Ok(result);
        }

        [HttpGet("info")]
        public ActionResult<SalonDto> Get([FromQuery] int idSalon)
        {

            var result = _salonService.GetOne(idSalon);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddSalon([FromBody] AddSalonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _salonService.Add(dto);

            return Created($"api / salon/{id}", null);
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] int idSalon)
        {
            var isDelete = _salonService.Delete(idSalon);

            if (!isDelete)
                return NotFound();

            return NoContent();
        }

        [HttpGet("rim")]
        public ActionResult<RimsDto> GetAllRim([FromQuery] int idSalon)
        {
            var result = _rimService.GetAllRim(idSalon);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        [HttpPost("rim")]
        public ActionResult AddRim([FromBody] AddRimDto dto, [FromRoute] int idSalon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _rimService.AddRim(dto, idSalon);

            return Created($"api /salon/{id}/rim", null);

        }

    }
}
