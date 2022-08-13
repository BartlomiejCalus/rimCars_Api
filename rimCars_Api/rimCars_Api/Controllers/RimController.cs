using rimCars_Api.Models;
using rimCars_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace rimCars_Api.Controllers
{
    [Route("att/rim")]
    public class RimController : ControllerBase
    {
        private readonly IRimService _rimService;

        public RimController(IRimService rimService)
        {
            _rimService = rimService;
        }

        [HttpGet]
        public ActionResult<RimsDto> GetAllRim([FromQuery] int idSalon)
        {
            var result = _rimService.GetAllRim(idSalon);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }

        [HttpPost]
        public ActionResult AddRim([FromBody] AddRimDto dto, [FromQuery] int idSalon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _rimService.AddRim(dto, idSalon);

            return Created($"api /salon/{id}/rim", null);

        }

        [HttpDelete]
        public ActionResult DeleteRim([FromQuery] int idRim)
        {
            var isDelete = _rimService.Delete(idRim);

            if (!isDelete)
                return NotFound();

            return NoContent();
        }
    }
}
