using DataBase.DTO;
using Lesson19Test.DataBase.Model.DTO;
using Lesson19Test.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lesson19Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] UserDto request)
        {
            var response = _userService.Login(request.Username, request.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost("Signup")]
        public ActionResult<ResponseDto> Signup([FromBody] UserDto request)
        {
            var response = _userService.Signup(request.Username, request.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}
