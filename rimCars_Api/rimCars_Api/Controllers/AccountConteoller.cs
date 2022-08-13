﻿using Microsoft.AspNetCore.Mvc;
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

    }
}
