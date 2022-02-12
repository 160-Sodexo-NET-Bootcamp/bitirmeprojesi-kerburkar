using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using Services.Utilities.Services;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;
        public AuthController(IAuthService authService, IMailService mailService)
        {
            _authService = authService;
            _mailService = mailService;
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
            if(result.IsSuccess)
            {               
                var accessToken = _authService.CreateToken(result.Data);
                return Ok(accessToken);
            }
            return BadRequest(result);
        }
    }
}
