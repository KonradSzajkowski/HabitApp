using System.Threading.Tasks;
using HabitApp.API.Dtos;
using HabitApp.API.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HabitApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        IAuthRepository repository;
        public AuthController(IAuthRepository repository){
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("register")]

        public async Task<ActionResult> Register (UserToRegisterDto RegisterData){
            if(await repository.IsUserExist(RegisterData.Username)) 
                return BadRequest("User Alerady Exist");
            var user =await repository.Register(RegisterData.Username,RegisterData.Password,RegisterData.eMail);
                return StatusCode(201); 
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login (UserToLogInDto user){
            var userFromRepo =await repository.Login(user.Username,user.Password);
            if(userFromRepo==null) return Unauthorized();
            var token=repository.GenerateToken(userFromRepo);
            return Ok(new{token});
        }

    }
}