using AuthorizationApp.Data;
using AuthorizationApp.Data.Dtos;
using AuthorizationApp.Models;
using AuthorizationApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class AuthController : Controller
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
           
        }
        [HttpPost("login")]
        public ActionResult Login(LoginDto loginDto)
        {
            var userToLogin = _authService.Login(loginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("register")]
        public ActionResult Register(RegisterDto registerDto)
        {
            var isUserExists = _authService.GetByMail(registerDto.Email);
            if (!isUserExists.Success)
            {
                return BadRequest("Kullanıcı Zaten Var !!");
            }
            var register = _authService.Register(registerDto, registerDto.Password);
            var result = _authService.CreateAccessToken(register.Data);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest( result.Message);
        }

    }
}
