using Microsoft.AspNetCore.Mvc;
using rimCars_Api.Models;
using rimCars_Api.Services;

namespace rimCars_Api.Controllers
{
    [Route("att/account")]
    [ApiController]
    public class AccountConteoller : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountConteoller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            if(token == null)
            {
                return BadRequest("Wrong email or password");
            }
            return Ok(token);
        }

    }
}
