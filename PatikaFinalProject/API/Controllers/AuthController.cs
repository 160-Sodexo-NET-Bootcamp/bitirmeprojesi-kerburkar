using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService; 
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerdto)
        {
            var result = await _authService.Register(registerdto);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logindto)
        {
            var result = await _authService.Login(logindto);
            if(result == "Success")
            {
                var user = await _authService.GetUserByEmail(logindto.Email);
                var accessToken = _authService.CreateToken(user);
                return Ok(accessToken);
            }
            return BadRequest(result);


        }


    }
}
