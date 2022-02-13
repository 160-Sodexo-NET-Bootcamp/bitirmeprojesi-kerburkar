using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("myoffers")]
        public async Task<IActionResult> MyOffers()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _accountService.MyOffers(userIntId);
            return Ok(result);
        }

        [HttpGet("incommingoffers")]
        public async Task<IActionResult> InCommingOffers()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _accountService.IncomingOffers(userIntId);
            return Ok(result);
        }

        [HttpPost("buyproduct")]
        public async Task<IActionResult> BuyProduct(int offerId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _accountService.BuyProduct(offerId,userIntId);
            return Ok(result);
        }

        [HttpPost("changes")]
        public async Task<IActionResult> ChangeOfferStatus(int offerId,int statusId)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _accountService.BuyProduct(offerId, userIntId);
            return Ok(result);
        }
    }
}
